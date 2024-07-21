using Newtonsoft.Json;
using RFIDApp.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class HomeRepository
    {
        private static HomeRepository _Instance;
        public static HomeRepository GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new HomeRepository();
            }
            return _Instance;
        }
        public async Task<List<ReportModel>> GetHomeData()
        {
            var result = new List<ReportModel>();
            string URL = "Ticket/getHomeData";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<List<ReportModel>>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<int> GetTotalTicket()
        {
            int result = 0;
            string URL = "Ticket/getTotalTicketToday";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<int>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<int> GetTotalOcation()
        {
            int result = 0;
            string URL = "Ticket/getTotalLocationToday";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<int>(jsonResult); ;

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
