using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAll(string token);
        Task<Course> GetById(int id);
        Task<Course> Insert(Course obj);
        Task<Course> Update(Course obj);
        Task Delete(int id);
        Task<CourseWithStudent> CourseWithStudent(int id);
    }
}
