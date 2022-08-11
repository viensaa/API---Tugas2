using Newtonsoft.Json;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Services
{
    public class CourseServices : ICourse
    {


        public Task<CourseWithStudent> CourseWithStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public  async Task<IEnumerable<Course>> GetAll()
        {
            List<Course> courses = new List<Course>();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:8001/api/Course")){

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                }                
            }
            return courses;
        }

        public Task<Course> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> Insert(Course obj)
        {
            throw new NotImplementedException();
        }

        public Task<Course> Update(Course obj)
        {
            throw new NotImplementedException();
        }
    }
}
