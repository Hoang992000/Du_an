using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.PaymentInfo;

namespace TicketManagement.ViewModel.PaymentInfo
{
    public class PaymentInfoViewModel : BaseViewModel
    {
        private ObservableCollection<PaymentInfoModel> _LstInfo;

        public ObservableCollection<PaymentInfoModel> ListInfo
        {
            get { return _LstInfo; }
            set { _LstInfo = value; OnPropertyChanged(); }
        }
        PaymentInfoRepository paymentInfoRepository { get; set; }
        public ICommand EditCommand { get; set; }
        public PaymentInfoViewModel()
        {
            paymentInfoRepository = PaymentInfoRepository.GetInstance();
            Task.Run(Innit);
            EditCommand = new RelayCommand<PaymentInfoModel>((u) => true, (u) => ExecuteEdit(u));
        }
        private async void Innit()
        {
            ListInfo = new ObservableCollection<PaymentInfoModel>(await paymentInfoRepository.getInfo());
        }
        private void ExecuteEdit(PaymentInfoModel model)
        {
            EditPaymentInfoView view = new EditPaymentInfoView(model);
            bool? result = view.ShowDialog();
            if (result == true)
            {
                Innit();
            }
        }
    }
}
