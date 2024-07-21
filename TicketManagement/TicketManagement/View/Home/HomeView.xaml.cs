using System.Windows.Controls;
using TicketManagement.ViewModel.Home;

namespace TicketManagement.View.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    }
}
