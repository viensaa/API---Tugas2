using Newtonsoft.Json;
using System.Text;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class CourseServices : ICourse
    {
        public async Task<CourseWithStudent> CourseWithStudent(int id,string token)
        {
            
            CourseWithStudent courseWithStudent = new CourseWithStudent();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:8001/api/Course/WithStudentByid/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        courseWithStudent = JsonConvert.DeserializeObject<CourseWithStudent>(apiResponse);
                    }
                }
            }
            return courseWithStudent;
        }

        public async Task Delete(int id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:8001/api/Course?id={id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("Gagal Menghapus data");
                    }
                }
            }
        }

        public  async Task<IEnumerable<Course>> GetAll(string token)
        {
            List<Course> courses = new List<Course>();
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",$"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:8001/api/Course")){

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                }                
            }
            return courses;
        }

        public async Task<Course> GetById(int id,string token)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:8001/api/Course/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<Course> Insert(Course obj, string token)
        {
            Course course = new Course();
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:8001/api/Course", content))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<Course> Update(Course obj,string token)
        {
            Course course = await GetById(obj.courseID,token);
            if (course == null)
                throw new Exception($"Id {obj.courseID} Tidak Ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj)
               , Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:8001/api/Course", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    course = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return course;
        }
        
    }
}
