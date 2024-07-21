using RFIDApp.Components;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel.PaymentInfo
{
    internal class EditPaymentInfoViewModel : BaseViewModel
    {
        private PaymentInfoModel _CurrentInfo { get; set; }
        public PaymentInfoModel CurrentInfo { get { return _CurrentInfo; } set { _CurrentInfo = value; OnPropertyChanged(); } }
        private string _appId { get; set; }

        public string appId
        {
            get { return _appId; }
            set { _appId = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        private string _merchantName { get; set; }

        public string merchantName
        {
            get { return _merchantName; }
            set { _merchantName = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        private string _merchantType { get; set; }

        public string merchantType
        {
            get { return _merchantType; }
            set { _merchantType = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        private string _merchantCode { get; set; }

        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        private string _terminalId { get; set; }

        public string terminalId
        {
            get { return _terminalId; }
            set { _terminalId = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        private string _secretKey { get; set; }

        public string secretKey
        {
            get { return _secretKey; }
            set { _secretKey = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        public string Title { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private PaymentInfoRepository paymentInfoRepository { get; set; }
        public EditPaymentInfoViewModel(PaymentInfoModel info)
        {
            CurrentInfo = info;
            paymentInfoRepository = PaymentInfoRepository.GetInstance();
            Title = "THÔNG TIN THANH TOÁN";
            appId = CurrentInfo.appId;
            merchantName = CurrentInfo.merchantName;
            merchantCode = CurrentInfo.merchantCode;
            merchantType = CurrentInfo.merchantType;
            terminalId = CurrentInfo.terminalId;
            secretKey = CurrentInfo.secretKey;
            CancelCommand = new RelayCommand<Window>((w) => true, (w) => w.Close());
            SaveCommand = new RelayCommand<Window>((w) => CanSave(), (w) => ExecuteSave(w));
        }
        private bool CanSave()
        {
            if (string.IsNullOrEmpty(appId) ||
                string.IsNullOrEmpty(merchantName) ||
                string.IsNullOrEmpty(merchantCode) ||
                string.IsNullOrEmpty(merchantType) ||
                string.IsNullOrEmpty(terminalId) ||
                string.IsNullOrEmpty(secretKey))
            {
                return false;
            }
            return true;
        }
        private async void ExecuteSave(Window w)
        {
            var result = await paymentInfoRepository.UpdateInfo(new PaymentInfoModel()
            {
                VnpayId = 1,
                appId = appId,
                merchantName = merchantName,
                merchantCode = merchantCode,
                merchantType = merchantType,
                terminalId = terminalId,
                secretKey = secretKey
            });
            if (result.VnpayId != null)
            {
                w.DialogResult = true;
                w.Close();
            }
            else new CustomMessageBox("Cập nhật thất bại", MessageType.Error, MessageButtons.Ok).ShowDialog();
        }
    }
}
