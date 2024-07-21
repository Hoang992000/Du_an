using RFIDApp.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.Location;

namespace TicketManagement.ViewModel.Location
{
    public class LocationViewModel : BaseViewModel
    {
        private ObservableCollection<LocationModel> _ListLocation;
        private PaginationModel _Pagination;
        private string _SearchLocation;
        public string SearchLocation { get => _SearchLocation; set { _SearchLocation = value; OnPropertyChanged(); } }
        public ObservableCollection<LocationModel> ListLocation { get => _ListLocation; set { _ListLocation = value; OnPropertyChanged(); } }
        public PaginationModel Pagination { get => _Pagination; set { _Pagination = value; OnPropertyChanged(); } }

        private LocationRepository Repository;

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public LocationViewModel()
        {
            Repository = LocationRepository.GetInstance();
            AddCommand = new RelayCommand<object>((p) => true, (p) => ExecuteAdd());
            EditCommand = new RelayCommand<LocationModelPage>((u) => true, (u) => ExecuteEdit(u));
            SearchCommand = new RelayCommand<object>((p) => true, (p) => ExecuteSearch(p));
            DeleteCommand = new RelayCommand<LocationModelPage>((p) => true, (p) => ExecuteDelete(p));
            Task.Run(Initialize);
        }
        private void ExecuteAdd()
        {
            AddOrEditLocation window = new AddOrEditLocation();
            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadPageUser();
            }
        }
        private void ExecuteSearch(object p)
        {
            Pagination.CurrentPage = 1;
            LoadPageUser(SearchLocation);
            SearchLocation = string.Empty;
        }
        private async void ExecuteDelete(LocationModelPage p)
        {
            bool? result = new CustomMessageBox("Bạn muốn xoá địa điểm này à?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (result.Value)
            {
                string res = await Repository.DeleteLocation(p);
                if (res == "Success")
                {
                    LoadPageUser();
                }
                MessageBox.Show(res, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExecuteEdit(LocationModelPage u)
        {
            AddOrEditLocation window = new AddOrEditLocation(u);
            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadPageUser();
            }
        }
        private async void Initialize()
        {
            try
            {
                IsExecuting = true;
                Pagination = new PaginationModel();
                Pagination.PageChanged += () =>
                {
                    LoadPageUser();
                };
                LoadPageUser();

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
                    ListLocation = new ObservableCollection<LocationModel>(await Repository.GetListData(Pagination.PageSize, Pagination.CurrentPage));
                }
                else
                {
                    ListLocation = new ObservableCollection<LocationModel>(await Repository.SearchData(userName, Pagination.PageSize, Pagination.CurrentPage));

                }

                if (ListLocation.Count > 0)
                {
                    int Total = await Repository.GetTotalRow();
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
