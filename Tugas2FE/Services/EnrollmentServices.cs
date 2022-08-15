using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class EnrollmentServices : IEnrollment
    {
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EnrollmentDetail>> GetAll()
        {
            List<EnrollmentDetail> enrollmentDetails = new List<EnrollmentDetail>();
            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("https://localhost:8001/api/Enrollment"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    enrollmentDetails = JsonConvert.DeserializeObject<List<EnrollmentDetail>>(apiResponse);
                }
            }
            return enrollmentDetails;

        }

        public Task<EnrollmentDetail> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            Enrollment enrollment = new Enrollment();
            using(var httpClient  = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:8001/api/Enrollment", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }
                }
            }
            return enrollment;
        }

        public Task<Enrollment> Update(Enrollment obj)
        {
            throw new NotImplementedException();
        }
    }
}
