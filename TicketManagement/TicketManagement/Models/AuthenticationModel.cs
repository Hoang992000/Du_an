using RFIDApp.Helper;
using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TicketManagement.Repositories;
using TicketManagement.ViewModel;

namespace TicketManagement.Models
{
    public class AuthenticatorModel : BaseViewModel
    {
        private bool _IsLoggedIn;
        public bool IsLoggedIn { get => _IsLoggedIn; set { _IsLoggedIn = value; StateLoggedInChanged?.Invoke(null, EventArgs.Empty); OnPropertyChanged(); } }
        public event EventHandler StateLoggedInChanged;
        private LoginRespon _CurrentUser;
        public LoginRespon CurrentUser { get => _CurrentUser; set { _CurrentUser = value; OnPropertyChanged(); } }


        private UserRepository userRepository;
        private static AuthenticatorModel _Ins;
        public static AuthenticatorModel Ins { get { if (_Ins == null) _Ins = new AuthenticatorModel(); return _Ins; } set { _Ins = value; } }
        private AuthenticatorModel()
        {
            userRepository = new UserRepository();
            IsLoggedIn = false;
        }
        public async Task<string> Login(string UserName, string PassWord)
        {
            try
            {
                LoginRespon respon = await userRepository.LoginAsync(UserName, PassWord);
                if (!string.IsNullOrEmpty(respon.Token))
                {
                    GenericIdentity identity = new GenericIdentity(respon.UserId.ToString());
                    if (respon.IsAdmin || respon.IsBoss)
                    {
                        CurrentUser = respon;
                        Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] { respon.IsBoss ? "IsBoss" : "IsAdmin" });
                        ApiHelper.SetTokenHeader(respon.Token);
                        IsLoggedIn = true;
                    }
                    else
                        MessageBox.Show("Bạn không có quyền truy cập", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(respon.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return "";
        }

        public async void Logout()
        {
            int res = await userRepository.Logout(CurrentUser.UserId);
            if (res == 1)
            {
                IsLoggedIn = false;
            }
            else
            {
                MessageBox.Show("Đăng xuất thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
