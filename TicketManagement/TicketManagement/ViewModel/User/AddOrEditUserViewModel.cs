using RFIDApp.Components;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel.User
{
    internal class AddOrEditUserViewModel : BaseViewModel
    {
        private PageUserModel _CurrentUser;
        public PageUserModel CurrentUser { get => _CurrentUser; set { _CurrentUser = value; OnPropertyChanged(); } }
        private string _UserName;
        private string _Password;
        private bool _IsAdmin;
        private string _FullName;
        public bool IsAdmin { get => _IsAdmin; set { _IsAdmin = value; OnPropertyChanged(); } }
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public string Title { get; set; }
        public bool IsNew { get; set; }
        private bool _IsChange { get; set; }
        public bool IsChange
        {
            get { return _IsChange; }
            set { _IsChange = value; ; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private UserRepository userRepository;
        private AuthenticatorModel Authenticator;
        public AddOrEditUserViewModel(PageUserModel user)
        {
            Authenticator = AuthenticatorModel.Ins;
            CurrentUser = user;
            Task.Run(Initialize);
            SaveCommand = new RelayCommand<Window>((w) => CanSave(), (w) => ExecuteSave(w));
            CancelCommand = new RelayCommand<Window>((w) => CanClose(), (w) => ExecuteClose(w));
        }
        private void Initialize()
        {
            userRepository = new UserRepository();
            if (CurrentUser != null)
            {
                Title = "EDIT USER";
                UserName = CurrentUser.UserName;
                FullName = CurrentUser.FullName;
                IsAdmin = CurrentUser.RoleName == "Admin" ? true : false;
                IsNew = false;
                if (Authenticator.CurrentUser.UserId == CurrentUser.UserId)
                {
                    IsChange = false;
                }
                else IsChange = true;
            }
            else
            {
                Title = "ADD USER";
                IsNew = true;
                IsChange = true;
            }
        }
        private void ExecuteClose(Window w)
        {
            w.Close();
        }

        private bool CanClose()
        {
            return true;
        }

        private bool CanSave()
        {
            if ((string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)))
                return false;
            return true;
        }

        private void ExecuteSave(Window w)
        {
            if (IsNew)
            {
                SaveNewUser(w);
            }
            else
            {
                UpdateUser(w);
            }

        }
        private async void UpdateUser(Window w)
        {
            try
            {
                UserModel user = new UserModel();
                user.UserId = CurrentUser.UserId;
                user.UserName = UserName;
                user.Pass = Password;
                user.RoleId = IsAdmin ? 1 : 2;
                UserModel result = await userRepository.UpdateUser(user);
                if (result.UserId != 0)
                {
                    w.DialogResult = true;
                    w.Close();
                }
                else new CustomMessageBox(result.UserName, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
        private async void SaveNewUser(Window w)
        {
            try
            {
                UserModel model = new UserModel()
                {
                    UserName = UserName,
                    Pass = Password,
                    RoleId = IsAdmin ? 1 : 2
                };
                UserModel res = await userRepository.AddUser(model);
                if (res.UserId != 0)
                {
                    w.DialogResult = true;
                    w.Close();
                }
                else
                {
                    new CustomMessageBox(res.UserName, MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
