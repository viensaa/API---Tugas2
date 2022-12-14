using Microsoft.EntityFrameworkCore;
using Tugas2BE.Interface;
using Tugas2BE.Models;

namespace Tugas2BE.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly AppDbContext _context;

        public EnrollmentDAL(AppDbContext dataContext)
        {
            _context = dataContext;
        }
        public async Task DeleteById(int id)
        {
            var findData = await _context.Enrollments.SingleOrDefaultAsync(e => e.EnrollmentID == id);
            if (findData == null) throw new Exception($"Data dengan ID {id} Tidak Ditemukan");

            _context.Remove(findData);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _context.Enrollments.Include(e=> e.Course).Include(c=>c.Student).OrderBy(e => e.EnrollmentID).ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var findData = await _context.Enrollments.SingleOrDefaultAsync(e => e.EnrollmentID == id);
            if (findData == null) throw new Exception($"Data dengan ID {id} Tidak Ditemukan");

            return findData;
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _context.Enrollments.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Enrollment> Update(Enrollment obj)
        {
            try
            {
                var findData = await _context.Enrollments.SingleOrDefaultAsync(s => s.EnrollmentID == obj.EnrollmentID);
                if (findData == null) throw new Exception($"Data dengan ID {obj.EnrollmentID} Tidak Ditemukan");

                findData.CourseID = obj.CourseID;
                findData.StudentID = obj.StudentID;
                findData.Grade = obj.Grade;

                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}