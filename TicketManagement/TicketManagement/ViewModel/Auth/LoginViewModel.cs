using RFIDApp.Components;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _UserName;
        private string _PassWord;
        private string _btnLoginText = "Đăng nhập";
        private UserRepository userRepository;

        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }
        public string btnLoginText { get => _btnLoginText; set { _btnLoginText = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SettingDBCommand { get; set; }
        private AuthenticatorModel Authentication;
        //MainView mainView;
        public LoginViewModel(AuthenticatorModel authen)
        {
            Initialize(authen);

            LoginCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord) || IsExecuting == true)
                    return false;
                return true;
            }, async (p) =>
            {
                btnLoginText = "Đăng nhập...";
                IsExecuting = true;
                await Authentication.Login(UserName, PassWord);
                IsExecuting = false;
                btnLoginText = "Đăng nhập";
            });
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                bool? result = new CustomMessageBox("Bạn muốn thoát ra à?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (result.Value)
                {
                    p.Close();
                }
            });

            //SettingDBCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    SettingDBView view = new SettingDBView();
            //    view.ShowDialog();
            //});
        }

        private void Initialize(AuthenticatorModel authen)
        {
            IsExecuting = true;
            Authentication = authen;
            userRepository = new UserRepository();
            IsExecuting = false;
        }
    }
}
