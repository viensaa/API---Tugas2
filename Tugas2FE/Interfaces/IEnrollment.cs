using Tugas2FE.ViewModels;

namespace Tugas2FE.Interfaces
{
   

    public interface IEnrollment
    {
        Task<IEnumerable<EnrollmentDetail>> GetAll(string token);
        Task<Enrollment> GetById(int id,string token);
        Task<Enrollment> Insert(Enrollment obj, string token);
        Task<Enrollment> Update(Enrollment obj, string token);
        Task Delete(int id, string token);


    }
}
