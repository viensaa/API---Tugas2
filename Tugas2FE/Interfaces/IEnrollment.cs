using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
   

    public interface IEnrollment
    {
        Task<IEnumerable<EnrollmentDetail>> GetAll();
        Task<Enrollment> GetById(int id);
        Task<Enrollment> Insert(Enrollment obj);
        Task<Enrollment> Update(Enrollment obj);
        Task Delete(int id);


    }
}
