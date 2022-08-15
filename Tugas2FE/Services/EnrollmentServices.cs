using Newtonsoft.Json;
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

        public Task<Enrollment> Insert(Enrollment obj)
        {
            throw new NotImplementedException();
        }

        public Task<Enrollment> Update(Enrollment obj)
        {
            throw new NotImplementedException();
        }
    }
}
