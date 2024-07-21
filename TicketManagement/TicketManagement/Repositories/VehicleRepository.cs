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
    public class VehicleRepository
    {
        private static VehicleRepository _Instance;
        public static VehicleRepository GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new VehicleRepository();
            }
            return _Instance;
        }
        public async Task<List<VehicleModel>> getListvehicle()
        {
            string URL = "Vehicle/getALL";
            var result = new List<VehicleModel>();
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<VehicleModel>>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<VehicleModel> AddVehicle(VehicleModel vehicle)
        {
            string URL = "Vehicle/add";
            VehicleModel result = new VehicleModel();
            var jsonStr = JsonConvert.SerializeObject(vehicle);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult);
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<VehicleModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        internal async Task<VehicleModel> UpdateVehicle(VehicleModel vehicle)
        {
            VehicleModel result = new VehicleModel();
            string URL = "Vehicle/update";
            var jsonStr = JsonConvert.SerializeObject(vehicle);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult)
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<VehicleModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        internal async Task<string> DeleteVehicle(VehicleModel p)
        {
            string BASE_URL = "Location/del?id=" + p.VehicleId;
            string result = "";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(BASE_URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult);
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<string>(jsonResult); ;

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
