using System.Windows;
using TicketManagement.ViewModel;

namespace TicketManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Models.AuthenticatorModel authen)
        {
            InitializeComponent();
            DataContext = new MainViewModel(authen);
        }
    }
}
