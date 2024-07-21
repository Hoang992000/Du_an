using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.ViewModel.Location;

namespace TicketManagement.View.Location
{
    /// <summary>
    /// Interaction logic for AddOrEditLocation.xaml
    /// </summary>
    public partial class AddOrEditLocation : Window
    {
        public AddOrEditLocation(LocationModelPage loca = null)
        {
            InitializeComponent();
            DataContext = new AddOrEditLocationViewModel(loca);
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
