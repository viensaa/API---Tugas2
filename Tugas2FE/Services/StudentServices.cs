using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{

    public class StudentServices : IStudent
    {
        
        public async Task Delete(int id,string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:8001/api/Student?id={id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("Gagal Menghapus data");
                    }
                }
            }
        }

        public async Task<IEnumerable<Student>> GetAll(string token)
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:8001/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }

        public async Task<Student> GetById(int id,string token)
        {
            Student student = new Student();
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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

        public async Task<StudentWithCourse> StudentCourseById(int id,string token)
        {
            StudentWithCourse studentWithCourse = new StudentWithCourse();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:8001/api/Student/WithCoursebyid/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        studentWithCourse = JsonConvert.DeserializeObject<StudentWithCourse>(apiResponse);
                    }
                }
            }
            return studentWithCourse;
        }

        public async Task<Student> Insert(Student obj,string token)
        {
            Student student = new Student();
            using ( var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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

        

        public async Task<Student> Update(Student obj,string token)
        {
            Student student = await GetById(obj.id,token);
            if(student == null)
                throw new Exception($"Id {obj.id} Tidak Ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj)
               , Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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
