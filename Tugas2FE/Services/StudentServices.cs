using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class StudentServices : IStudent
    {
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:8001/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }

        public async Task<Student> GetById(int id)
        {
            Student student = new Student();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:8001/api/Student/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<Student> Insert(Student obj)
        {
            Student student = new Student();
            using ( var httpClient = new HttpClient())
            {
                //serialize object dri json ke c#
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:8001/api/Student", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<Student> Update(Student obj)
        {
            Student student = await GetById(obj.id);
            if(student == null)
                throw new Exception($"Id {obj.id} Tidak Ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj)
               , Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:8001/api/Student", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return student;
        }

    }
}
