using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RFIDApp.Helper
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //ApiClient.BaseAddress = new Uri("http://localhost:5220/api/");
            ApiClient.BaseAddress = new Uri("http://vnpay.rfidstore.vn/api/");
        }

        public static void SetTokenHeader(string token)
        {
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
