using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class AccountServices : IAccount
    {
        public Task<SLogin> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
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
