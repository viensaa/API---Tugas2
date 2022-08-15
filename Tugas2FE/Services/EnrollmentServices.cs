using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class EnrollmentServices : IEnrollment
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:8001/api/Enrollment?id={id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("Gagal Menghapus data");
                    }
                }
            }
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

        public async Task<Enrollment> GetById(int id)
        {
            Enrollment enrollment = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:8001/api/Enrollment/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }
                }
            }
            return enrollment;
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

        public async Task<Enrollment> Update(Enrollment obj)
        {
            Enrollment enrollment = await GetById(obj.EnrollmentID);
            if(enrollment == null)
            {
                throw new Exception($"Id {obj.EnrollmentID} Tidak Ditemukan");
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj)
              , Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:8001/api/Enrollment", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                }
            }
            return enrollment;
        }



    }
}
