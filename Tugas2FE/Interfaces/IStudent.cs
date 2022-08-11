using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> Insert(Student obj);
        Task<Student> Update(Student obj);
        Task Delete(int id);

    }
}
