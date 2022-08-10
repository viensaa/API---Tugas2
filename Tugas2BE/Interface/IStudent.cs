using Tugas2BE.Models;

namespace Tugas2BE.Interface
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> ByFirstName(string Fname);
        Task<IEnumerable<Student>> ByLastName(string Lname);
        Task<IEnumerable<Student>> StudentWithCourse(int page);

    }
}
