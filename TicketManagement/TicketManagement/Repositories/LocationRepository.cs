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
    public class LocationRepository
    {
        private static LocationRepository _Instance;
        public static LocationRepository GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new LocationRepository();
            }
            return _Instance;
        }
        public async Task<int> GetTotalRow()
        {
            int result = 0;
            string URL = "Location/coutRow";
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
        public async Task<List<LocationModel>> getListLocation()
        {
            var result = new List<LocationModel>();
            string URL = "Location/GetAllLocationForCBB";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<LocationModel>>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<LocationModelPage>> GetListData(int size, int num)
        {
            var tmp = new List<LocationModel>();
            var result = new List<LocationModelPage>();
            string URL = "Location/GetAll?size=" + size + "&num=" + num;
            int stt = (num * size) - (size - 1);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<LocationModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        result.Add(new LocationModelPage { LocationId = x.LocationId, Locationname = x.Locationname, Status = x.IsActive == true ? "Đang hoat động" : "Không hoạt động", STT = stt });
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
        public async Task<List<LocationModelPage>> SearchData(string key, int size, int num)
        {
            var tmp = new List<LocationModel>();
            var result = new List<LocationModelPage>();
            string URL = "Location/find?key=" + key + "&size=" + size + "&num=" + num;
            int stt = (num * size) - (size - 1);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<LocationModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        result.Add(new LocationModelPage { LocationId = x.LocationId, Locationname = x.Locationname, Status = x.IsActive == true ? "Đang hoat động" : "Không hoạt động", STT = stt });
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

        public async Task<LocationModel> AddLocation(LocationModel locationModel)
        {
            LocationModel result = new LocationModel();
            string URL = "Location/add";
            var jsonStr = JsonConvert.SerializeObject(locationModel);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult);
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<LocationModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }

        internal async Task<LocationModel> UpdateLocation(LocationModel locationModel)
        {
            LocationModel result = new LocationModel();
            string URL = "Location/update";
            var jsonStr = JsonConvert.SerializeObject(locationModel);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult)
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<LocationModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }

        internal async Task<string> DeleteLocation(LocationModelPage p)
        {
            string BASE_URL = "Location/del?id=" + p.LocationId;
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
