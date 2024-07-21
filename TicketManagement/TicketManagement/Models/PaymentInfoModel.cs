namespace TicketManagement.Models
{
    public class PaymentInfoModel
    {
        public int VnpayId { get; set; }
        public string appId { get; set; }
        public string merchantName { get; set; }
        public string merchantType { get; set; }
        public string merchantCode { get; set; }
        public string terminalId { get; set; }
        public string secretKey { get; set; }
    }
}
