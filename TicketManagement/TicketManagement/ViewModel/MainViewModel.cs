using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.ViewModel.Home;
using TicketManagement.ViewModel.Location;
using TicketManagement.ViewModel.PaymentInfo;
using TicketManagement.ViewModel.Report;
using TicketManagement.ViewModel.Setting;
using TicketManagement.ViewModel.Ticket;
using TicketManagement.ViewModel.User;
using TicketManagement.ViewModel.Vehicle;

namespace TicketManagement.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private object _CurrentView;
        public object CurrentView { get => _CurrentView; set { _CurrentView = value; OnPropertyChanged(); } }
        public ICommand HomeViewCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SettingViewCommand { get; set; }
        public ICommand ReportViewCommand { get; set; }
        public ICommand PaymentInfoViewCommand { get; set; }
        public ICommand VehicleViewCommand { get; set; }
        public ICommand LocationViewCommand { get; set; }
        public ICommand TicketViewCommand { get; set; }
        public ICommand UserViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public UserViewModel UserVM { get; set; }
        public VehicleViewModel VehicleVM { get; set; }
        public LocationViewModel LocationVM { get; set; }
        public SettingViewModel SettingVM { get; set; }
        public PaymentInfoViewModel PaymentVM { get; set; }
        public TicketViewModel TicketVM { get; set; }
        public ReportViewModel ReportVM { get; set; }

        public AuthenticatorModel Authenticator;
        public MainViewModel(AuthenticatorModel authen)
        {
            Authenticator = authen;
            IsBoss = Authenticator.CurrentUser.IsBoss;
            CurrentView = new HomeViewModel();
            LogoutCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsExecuting = true;
                Authenticator.Logout();
            });

            ReportViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = ReportVM = new ReportViewModel();
            });

            PaymentInfoViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = PaymentVM = new PaymentInfoViewModel();
            });

            VehicleViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = VehicleVM = new VehicleViewModel();
            });

            UserViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = UserVM = new UserViewModel(Authenticator.CurrentUser);
            });

            HomeViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = HomeVM = new HomeViewModel();
            });

            LocationViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = LocationVM = new LocationViewModel();
            });

            TicketViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = TicketVM = new TicketViewModel();
            });

            SettingViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentView = SettingVM;
            });


        }
    }
}
