using DemoVnPay.Helper;
using DemoVnPay.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using TicketManagement.Model;

namespace DemoVnPay.Repository
{
    public class VnpayRepository
    {
        private DataContext _context;
        public VnpayRepository(DataContext context)
        {
            _context = context;
        }
        public string CheckTransaction(string OrderCode, string DateCheck, HttpContext httpContext)
        {
            var vnp_Api = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
            var vnp_HashSecret = "VT7VWX90ZUG1PLPB72JAYIVE7FMN6OZ2"; //Secret KEy
            var vnp_TmnCode = "CNMNMWTT"; // Terminal Id

            var vnp_RequestId = DateTime.Now.Ticks.ToString(); //Mã hệ thống merchant tự sinh ứng với mỗi yêu cầu truy vấn giao dịch. Mã này là duy nhất dùng để phân biệt các yêu cầu truy vấn giao dịch. Không được trùng lặp trong ngày.
            var vnp_Version = VnPayLibrary.VERSION; //2.1.0
            var vnp_Command = "querydr";
            var vnp_TxnRef = OrderCode; // Mã giao dịch thanh toán tham chiếu
            var vnp_OrderInfo = "Truy van giao dich:" + OrderCode;
            var vnp_TransactionDate = DateCheck;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = Utils.GetIpAddress(httpContext);
            var signData = vnp_RequestId + "|" + vnp_Version + "|" + vnp_Command + "|" + vnp_TmnCode + "|" + vnp_TxnRef + "|" + vnp_TransactionDate + "|" + vnp_CreateDate + "|" + vnp_IpAddr + "|" + vnp_OrderInfo;
            var vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);
            var qdrData = new
            {
                vnp_RequestId = vnp_RequestId,
                vnp_Version = vnp_Version,
                vnp_Command = vnp_Command,
                vnp_TmnCode = vnp_TmnCode,
                vnp_TxnRef = vnp_TxnRef,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_TransactionDate = vnp_TransactionDate,
                vnp_CreateDate = vnp_CreateDate,
                vnp_IpAddr = vnp_IpAddr,
                vnp_SecureHash = vnp_SecureHash

            };
            var jsonData = JsonConvert.SerializeObject(qdrData);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(vnp_Api);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var strData = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                strData = streamReader.ReadToEnd();
            }
            string res = handelStatusString(strData, OrderCode);
            return res;
        }
        private string handelStatusString(string jsonString, string code)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            //string format = "yyyyMMddHHmmss";
            // Lấy ra giá trị của thuộc tính vnp_TransactionStatus
            try
            {
                string transactionStatus = (string)jsonObject["vnp_TransactionStatus"];
                //DateTime date = DateTime.ParseExact((string)jsonObject["vnp_PayDate"], format, null);
                //string datecheck = (string)jsonObject["vnp_PayDate"];
                //var ticket = _context.Tickets.Find(code);
                //ticket.DateToCheck = datecheck;
                //ticket.PaymentTime = date;
                //ticket.PaymentMethodId = 1;
                //ticket.TimeOut = DateTime.Now;
                // Kiểm tra nếu giá trị là "00", thực hiện cắt giá trị này ra khỏi chuỗi JSON
                if (transactionStatus == "00")
                {
                    //ticket.StatusOder = true;
                    //_context.Tickets.Update(ticket);
                    //_context.SaveChanges();
                    return "Thanh toán thành công";
                }
                //ticket.StatusOder = false;
                //_context.Tickets.Update(ticket);
                //_context.SaveChanges();
                return "Thanh toán thất bại";
            }
            catch
            {
                return "Có lỗi gì đó xảy ra";
            }
        }
        public string AddVnpayInfo(VnPayInfo vnPayInfo)
        {
            var check = _context.vnPayInfos.ToList();
            if (check.Count == 0)
            {
                _context.vnPayInfos.Add(vnPayInfo);
                _context.SaveChanges();
                return "Thêm thông tin thanh toán thành công";
            }
            else return "Đã tồn tại thông tin thanh toán";
        }
        public List<VnPayInfo> GetVnpayInfo()
        {
            var res = _context.vnPayInfos.ToList();
            return res;
        }
        public VnPayInfo UpdateIfo(VnPayInfo vnPayInfo)
        {
            var info = _context.vnPayInfos.Find(vnPayInfo.VnpayId);
            if (info == null)
            {
                return new VnPayInfo();
            }
            info.secretKey = vnPayInfo.secretKey;
            info.terminalId = vnPayInfo.terminalId;
            info.appId = vnPayInfo.appId;
            info.merchantName = vnPayInfo.merchantName;
            info.merchantCode = vnPayInfo.merchantCode;
            info.merchantType = vnPayInfo.merchantType;
            _context.vnPayInfos.Update(info);
            _context.SaveChanges();
            return info;
        }
        public string DeleteInfo(int id)
        {
            try
            {
                var us = _context.vnPayInfos.Find(id);
                _context.vnPayInfos.Remove(us);
                _context.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra");
            }
        }
    }
}
