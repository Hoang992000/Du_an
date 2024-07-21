using System.Windows;
using System.Windows.Input;
using TicketManagement.ViewModel;

namespace TicketManagement.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView(Models.AuthenticatorModel authen)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(authen);
        }
        private void loginWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
