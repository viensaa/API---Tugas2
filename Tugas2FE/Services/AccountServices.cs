using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class AccountServices : IAccount
    {
        public async Task<SLogin> Authenticate(Account obj)
        {
            SLogin sLogin = new SLogin();
            Account account = new Account();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                   Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"https://localhost:8001/api/User/Login", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        sLogin = JsonConvert.DeserializeObject<SLogin>(apiResponse);
                    }else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        throw new Exception($"{apiResponse}");
                    }
                }
            }
            return sLogin;
        }

        public async Task<Account> Insert(Account obj)
        {
            Account account = new Account();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"https://localhost:8001/api/User", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        account = JsonConvert.DeserializeObject<Account>(apiResponse);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Error : {apiResponse}");
                    }
                }
            }
            return account;
        }
    }
}
