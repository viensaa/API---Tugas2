using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAll(string token);
        Task<Student> GetById(int id,string token);
        Task<Student> Insert(Student obj, string token);
        Task<Student> Update(Student obj, string token);
        Task Delete(int id, string token);
        Task<StudentWithCourse> StudentCourseById(int id, string token);

    }
}
