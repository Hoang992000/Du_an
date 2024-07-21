using System.Windows;
using TicketManagement.ViewModel.Report;

namespace TicketManagement.View.Report
{
    /// <summary>
    /// Interaction logic for ReportByLocation.xaml
    /// </summary>
    public partial class ReportByLocation : Window
    {
        public ReportByLocation()
        {
            InitializeComponent();
            DataContext = new ReportByLocationViewModel();
        }
    }
}
