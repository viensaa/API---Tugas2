using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAll(string token);
        Task<Course> GetById(int id,string token);
        Task<Course> Insert(Course obj, string token);
        Task<Course> Update(Course obj, string token);
        Task Delete(int id, string token);
        Task<CourseWithStudent> CourseWithStudent(int id, string token);
    }
}
