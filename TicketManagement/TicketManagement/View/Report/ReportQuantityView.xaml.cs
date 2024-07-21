using System.Windows;
using TicketManagement.ViewModel.Report;

namespace TicketManagement.View.Report
{
    /// <summary>
    /// Interaction logic for ReportQuantityView.xaml
    /// </summary>
    public partial class ReportQuantityView : Window
    {
        public ReportQuantityView(int id)
        {
            InitializeComponent();
            DataContext = new ReportQuantityViewModel(id);
        }
    }
}
