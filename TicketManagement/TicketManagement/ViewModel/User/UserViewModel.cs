using RFIDApp.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.User;

namespace TicketManagement.ViewModel.User
{
    public class UserViewModel : BaseViewModel
    {
        private ObservableCollection<PageUserModel> _ListUser;
        private PaginationModel _Pagination;
        private string _SearchUser;
        public string SearchUser { get => _SearchUser; set { _SearchUser = value; OnPropertyChanged(); } }
        public ObservableCollection<PageUserModel> ListUser { get => _ListUser; set { _ListUser = value; OnPropertyChanged(); } }
        public PaginationModel Pagination { get => _Pagination; set { _Pagination = value; OnPropertyChanged(); } }

        private UserRepository userRepository;

        public ICommand AddUserCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private AuthenticatorModel Authenticator;
        private LoginRespon currentUser;
        public UserViewModel(LoginRespon _currentUser)
        {
            userRepository = new UserRepository();
            currentUser = _currentUser;
            Task.Run(Initialize);
            AddUserCommand = new RelayCommand<object>((p) => CanAddUser(), (p) => ExecuteAddUser());
            EditCommand = new RelayCommand<PageUserModel>((u) => CanEditUser(), (u) => ExecuteEditUser(u));
            SearchCommand = new RelayCommand<object>((p) => CanSearch(), (p) => ExecuteSearch(p));
            DeleteCommand = new RelayCommand<PageUserModel>((p) => true, (p) => ExecuteDelete(p));
        }

        private async void ExecuteDelete(PageUserModel p)
        {
            bool? result = new CustomMessageBox("Do you want to delete this user?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (result.Value)
            {
                string res = await userRepository.DeleteUser(p);
                if (res == "Success")
                {
                    LoadPageUser();
                }
                MessageBox.Show(res, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanSearch()
        {
            return true;
        }

        private void ExecuteSearch(object p)
        {
            Pagination.CurrentPage = 1;
            LoadPageUser(SearchUser);
            SearchUser = string.Empty;
        }

        private void ExecuteEditUser(PageUserModel u)
        {
            AddOrEditUserView window = new AddOrEditUserView(u);
            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadPageUser();
            }
        }

        private bool CanEditUser()
        {
            return true;
        }

        private bool CanAddUser()
        {
            if (IsExecuting)
                return false;
            return true;
        }

        private void ExecuteAddUser()
        {
            AddOrEditUserView window = new AddOrEditUserView();
            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadPageUser();
            }
        }

        private async void Initialize()
        {
            //var test = await userRepository.GetListUser(1, 2);
            try
            {
                IsExecuting = true;
                userRepository = new UserRepository();
                Pagination = new PaginationModel();
                Pagination.PageChanged += () =>
                {
                    LoadPageUser();
                };
                LoadPageUser();
                Authenticator = AuthenticatorModel.Ins;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsExecuting = false;
            }

        }

        private async void LoadPageUser(string userName = null)
        {
            try
            {
                IsExecuting = true;
                if (string.IsNullOrEmpty(userName))
                {
                    ListUser = new ObservableCollection<PageUserModel>(await userRepository.GetListUser(Pagination.PageSize, Pagination.CurrentPage, currentUser));
                }
                else
                {
                    ListUser = new ObservableCollection<PageUserModel>(await userRepository.SearchUserByUserName(userName, Pagination.PageSize, Pagination.CurrentPage, currentUser));

                }

                if (ListUser.Count > 0)
                {
                    int Total = await userRepository.GetTotalRow();
                    Pagination.InitPagination(Total);
                }
                else
                {
                    Pagination.InitPagination(0);
                }
            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                IsExecuting = false;

            }

        }
    }
}
