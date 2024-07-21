using Newtonsoft.Json;
using RFIDApp.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class Ticketrepository
    {
        private static Ticketrepository _Instance;
        public static Ticketrepository GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Ticketrepository();
            }
            return _Instance;
        }
        public async Task<int> GetTotalRow()
        {
            int result = 0;
            string URL = "Ticket/coutRow";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<int>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<TicketModelSTT>> reportdetail(int locationId, int vehicleId, DateTime start, DateTime end)
        {
            var tmp = new List<TicketModel>();
            var result = new List<TicketModelSTT>();
            int stt = 1;
            string URL = "Ticket/reportByTime?locaid=" + locationId + "&vehiid=" + vehicleId + "&from=" + start + "&to=" + end;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<TicketModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        result.Add(new TicketModelSTT
                        {
                            STT = stt,
                            TicketCode = x.TicketCode,
                            VehicleNum = x.VehicleNum,
                            VehicleType = x.VehicleType,
                            TimeIn = x.TimeIn,
                            TimeOut = x.TimeOut,
                            PaymentMethodType = x.PaymentMethodType,
                            PaymentTime = x.PaymentTime,
                            StatusOder = x.StatusOder,
                            LocationName = x.LocationName,
                            Cost = x.Cost
                        });
                        stt++;
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<TicketModelSTT>> GetAllData(int size, int num)
        {
            var tmp = new List<TicketModel>();
            var result = new List<TicketModelSTT>();
            string URL = "Ticket/report?size=" + size + "&num=" + num;
            int stt = (num * size) - (size - 1);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<TicketModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        result.Add(new TicketModelSTT
                        {
                            STT = stt,
                            TicketCode = x.TicketCode,
                            VehicleNum = x.VehicleNum,
                            VehicleType = x.VehicleType,
                            TimeIn = x.TimeIn,
                            TimeOut = x.TimeOut,
                            PaymentMethodType = x.PaymentMethodType,
                            PaymentTime = x.PaymentTime,
                            StatusOder = x.StatusOder,
                            LocationName = x.LocationName,
                            Cost = x.Cost,
                        });
                        stt++;
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<TicketModelSTT>> SearchUserByUserName(string key, int size, int num)
        {
            var tmp = new List<TicketModel>();
            var result = new List<TicketModelSTT>();
            string URL = "Ticket/findTicket?key=" + key + "&size=" + size + "&num=" + num;
            int stt = (num * size) - (size - 1);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<TicketModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        result.Add(new TicketModelSTT
                        {
                            STT = stt,
                            TicketCode = x.TicketCode,
                            VehicleNum = x.VehicleNum,
                            VehicleType = x.VehicleType,
                            TimeIn = x.TimeIn,
                            TimeOut = x.TimeOut,
                            PaymentMethodType = x.PaymentMethodType,
                            PaymentTime = x.PaymentTime,
                            StatusOder = x.StatusOder
                        });
                        stt++;
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<int> getRevenue(int locationId, int vehicleId, DateTime start, DateTime end)
        {
            string URL = "Ticket/Revenue?locaid=" + locationId + "&vehiid=" + vehicleId + "&from=" + start + "&to=" + end;
            int result = 0;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<int>(jsonResult);
                }
                return (int)result;
            }
        }
        public async Task<List<ReportByVehicle>> getReportQuantityVehicle(DateTime start, DateTime end)
        {
            string url = "Ticket/ReportByVehicle?from=" + start + "&to=" + end;
            List<ReportByVehicle> result = new List<ReportByVehicle>();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<ReportByVehicle>>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<ReportByLocation>> getReportQuantityLocation(DateTime start, DateTime end)
        {
            string url = "Ticket/ReportByLocation?from=" + start + "&to=" + end;
            List<ReportByLocation> result = new List<ReportByLocation>();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<ReportByLocation>>(jsonResult);
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
