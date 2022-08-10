using Tugas2BE.Models;

namespace Tugas2BE.Interface
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> ByTitle(string title);
        Task<IEnumerable<Course>> CourseByStudent();

    }
}
