using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.ViewModel.User;

namespace TicketManagement.View.User
{
    /// <summary>
    /// Interaction logic for AddOrEditUserView.xaml
    /// </summary>
    public partial class AddOrEditUserView : Window
    {
        public AddOrEditUserView(PageUserModel user = null)
        {
            InitializeComponent();
            DataContext = new AddOrEditUserViewModel(user);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
