using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace RFIDApp.Components
{
    /// <summary>
    /// Interaction logic for PaginationBar.xaml
    /// </summary>
    public partial class PaginationBar : UserControl
    {
        public PaginationBar()
        {
            InitializeComponent();
            cbPageSize.Items.Add(10);
            cbPageSize.Items.Add(20);
            cbPageSize.Items.Add(50);
            //cbPageSize.SetBinding(ComboBox.SelectedItemProperty, new System.Windows.Data.Binding("SelectedItem") { Mode = BindingMode.TwoWay });

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
