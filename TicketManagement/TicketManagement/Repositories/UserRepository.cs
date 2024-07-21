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
    public class UserRepository
    {
        public async Task<LoginRespon> LoginAsync(string username, string password)
        {
            string BASE_URL = "User/Login";
            LoginRespon result = new LoginRespon();
            LoginRequest login = new LoginRequest() { UserName = username, Pass = password, LocationId = 0 };
            var jsonStr = JsonConvert.SerializeObject(login);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(BASE_URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult);
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<LoginRespon>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                //else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                //{

                //        throw new Exception(res.Result.ToString());
                //}
                return result;
            }
        }
        public async Task<int> GetTotalRow()
        {
            int result = 0;
            string URL = "User/coutRow";
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
        public async Task<List<PageUserModel>> GetListUser(int size, int num, LoginRespon user)
        {
            var result = new List<PageUserModel>();
            var tmp = new List<UserModel>();
            string URL = "User/getAllandLoca?size=" + size + "&num=" + num + "&isBoss=" + user.IsBoss;
            int stt = (num * size) - (size - 1);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<UserModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        if (x.Location == null)
                        {
                            if (x.RoleId == 1 || x.RoleId == 3)
                            {
                                result.Add(new PageUserModel { FullName = x.FullName, STT = stt, UserId = x.UserId, RoleName = x.RoleId == 1 ? "Admin" : x.RoleId == 3 ? "Boss" : "User", UserName = x.UserName, LocationName = x.Status ? "Đang hoạt động" : "Không hoạt động" });
                            }
                            else
                            {
                                result.Add(new PageUserModel { FullName = x.FullName, STT = stt, UserId = x.UserId, RoleName = x.RoleId == 1 ? "Admin" : x.RoleId == 3 ? "Boss" : "User", UserName = x.UserName, LocationName = "Không hoạt động" });
                            }
                            stt++;
                        }
                        else
                        {
                            result.Add(new PageUserModel { FullName = x.FullName, STT = stt, UserId = x.UserId, RoleName = "User", UserName = x.UserName, LocationName = x.Location.Locationname });
                            stt++;
                        }
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<List<PageUserModel>> SearchUserByUserName(string name, int size, int num, LoginRespon user)
        {
            var result = new List<PageUserModel>();
            var tmp = new List<UserModel>();
            string URL = "User/searchUser?key=" + name + "&size=" + size + "&num=" + num + "&isBoss=" + user.IsBoss; ;
            int stt = (num * size) - (size - 1);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(URL))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    tmp = JsonConvert.DeserializeObject<List<UserModel>>(jsonResult);
                    tmp.ForEach(x =>
                    {
                        if (x.Location == null)
                        {
                            result.Add(new PageUserModel { FullName = x.FullName, STT = stt, UserId = x.UserId, RoleName = x.RoleId == 1 ? "Admin" : x.RoleId == 3 ? "Boss" : "User", UserName = x.UserName, LocationName = "Office" });
                            stt++;
                        }
                        else
                        {
                            result.Add(new PageUserModel { FullName = x.FullName, STT = stt, UserId = x.UserId, RoleName = x.RoleId == 1 ? "Admin" : x.RoleId == 3 ? "Boss" : "User", UserName = x.UserName, LocationName = x.Location.Locationname });
                            stt++;
                        }
                    });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                return result;
            }
        }
        public async Task<UserModel> AddUser(UserModel user)
        {
            string BASE_URL = "User/add";
            UserModel result = new UserModel();
            var jsonStr = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(BASE_URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult);
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<UserModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                //else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                //{

                //        throw new Exception(res.Result.ToString());
                //}
                return result;
            }
        }

        public async Task<UserModel> UpdateUser(UserModel user)
        {
            string BASE_URL = "User/update";
            UserModel result = new UserModel();
            var jsonStr = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(BASE_URL, content))
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                //ApiModel res = JsonConvert.DeserializeObject<ApiModel>(jsonResult);
                if (response.IsSuccessStatusCode)
                {

                    result = JsonConvert.DeserializeObject<UserModel>(jsonResult); ;

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                }
                //else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                //{

                //        throw new Exception(res.Result.ToString());
                //}
                return result;
            }
        }
        public async Task<string> DeleteUser(UserModel user)
        {
            string BASE_URL = "User/del?id=" + user.UserId;
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
        public async Task<int> Logout(int id)
        {
            string url = "User/logout?userID=" + id;
            int result = 0;
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
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
