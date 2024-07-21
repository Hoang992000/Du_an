using System.Windows;
using TicketManagement.Models;
using TicketManagement.ViewModel.PaymentInfo;

namespace TicketManagement.View.PaymentInfo
{
    /// <summary>
    /// Interaction logic for EditPaymentInfoView.xaml
    /// </summary>
    public partial class EditPaymentInfoView : Window
    {
        public EditPaymentInfoView(PaymentInfoModel paymentInfoModel)
        {
            InitializeComponent();
            DataContext = new EditPaymentInfoViewModel(paymentInfoModel);
        }
    }
}
