namespace DemoVnPay.Model
{
    public class VnpayPayment
    {
        public string code { get; set; }
        public string message { get; set; }
        public string msgType { get; set; }
        public string txnId { get; set; }
        public string qrTrade { get; set; }
        public string bankCode { get; set; }
        public string amount { get; set; }
        public string payDate { get; set; }
        public string merchantCode { get; set; }
        public string terminalId { get; set; }
        public string checksum { get; set; }
    }
}
