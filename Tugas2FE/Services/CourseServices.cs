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

        public Task<IEnumerable<Course>> GetAll()
        {
            throw new NotImplementedException();
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
