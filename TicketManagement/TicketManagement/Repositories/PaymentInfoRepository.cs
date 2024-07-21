using Newtonsoft.Json;
using RFIDApp.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class PaymentInfoRepository
    {
        private static PaymentInfoRepository _Instance;
        public static PaymentInfoRepository GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new PaymentInfoRepository();
            }
            return _Instance;
        }
        public async Task<List<PaymentInfoModel>> getInfo()
        {
            string URL = "VnPay/GetinfoVnpay";
            List<PaymentInfoModel> result = new List<PaymentInfoModel>();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<PaymentInfoModel>>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }

        public async Task<PaymentInfoModel> UpdateInfo(PaymentInfoModel paymentInfoModel)
        {
            PaymentInfoModel result = new PaymentInfoModel();
            string URL = "VnPay/update";
            var jsonStr = JsonConvert.SerializeObject(paymentInfoModel);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult)
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<PaymentInfoModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
    }
}
