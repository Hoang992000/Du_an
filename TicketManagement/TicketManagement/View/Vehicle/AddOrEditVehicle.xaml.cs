using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.ViewModel.Vehicle;

namespace TicketManagement.View.Vehicle
{
    /// <summary>
    /// Interaction logic for AddOrEditVehicle.xaml
    /// </summary>
    public partial class AddOrEditVehicle : Window
    {
        public AddOrEditVehicle(VehicleModel v = null)
        {
            InitializeComponent();
            DataContext = new AddOrEditVehicleViewModel(v);
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Kiểm tra xem ký tự có phải là số không
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            // Kiểm tra ký tự có phải là số không (sử dụng biểu thức chính quy)
            Regex regex = new Regex("[^0-9]+"); // Chỉ cho phép các ký tự số
            return !regex.IsMatch(text);
        }

        private void NumericTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
