using Newtonsoft.Json;
using RFIDApp.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class ReportRepository
    {
        private static ReportRepository _Instance;
        public static ReportRepository GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new ReportRepository();
            }
            return _Instance;
        }
        public async Task<List<ChartData>> GetChartDatasAsync()
        {
            string url = "Ticket/GetChartData";
            List<ChartData> result = new List<ChartData>();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<ChartData>>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<HomeChartData>> GetHomeChartDatasAsync()
        {
            string url = "Ticket/GetHomeChartData";
            List<HomeChartData> result = new List<HomeChartData>();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<HomeChartData>>(jsonResult);
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
