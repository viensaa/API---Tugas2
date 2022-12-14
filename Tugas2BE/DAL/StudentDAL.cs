using Microsoft.EntityFrameworkCore;
using Tugas2BE.Interface;
using Tugas2BE.Models;

namespace Tugas2BE.DAL
{
    public class StudentDAL : IStudent
    {
        private readonly AppDbContext _context;

        public StudentDAL(AppDbContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<IEnumerable<Student>> ByFirstName(string Fname)
        {
            var results = await _context.Students.Where(s => s.FirstMidName.Contains(Fname)).ToListAsync();
            if (results == null) throw new Exception($"Data Yang Mengandung Nama Depan Dengan {Fname} Tidak Ditemukan");

            return results;
        }

        public async Task<IEnumerable<Student>> ByLastName(string Lname)
        {
            var results = await _context.Students.Where(s => s.LastName.Contains(Lname)).ToListAsync();
            if (results == null) throw new Exception($"Data Yang Mengandung Nama Belakang Dengan {Lname} Tidak Ditemukan");

            return results;
        }

        public async Task DeleteById(int id)
        {
            try
            {
                var findData = await _context.Students.SingleOrDefaultAsync(s => s.ID == id);
                if (findData == null) throw new Exception($"Data dengan ID {id} Tidak Ditemukan");

                _context.Remove(findData);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _context.Students.OrderBy(s => s.FirstMidName).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Students.SingleOrDefaultAsync(s => s.ID == id);
            if (result == null) throw new Exception($"Data dengan ID {id} Tidak Ditemukan");

            return result;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _context.Students.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> StudentCourseById(int id)
        {
            var result = await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course)
               .SingleOrDefaultAsync(s => s.ID == id);
               
            return result;
        }

        public async Task<IEnumerable<Student>> StudentWithCourse(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Students.Count() / pageResults);

            var results = await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            return results;
        }

        public async Task<Student> Update(Student obj)
        {
            try
            {
                var findData = await _context.Students.SingleOrDefaultAsync(s => s.ID == obj.ID);
                if (findData == null) throw new Exception($"Data dengan ID {obj.ID} Tidak Ditemukan");

                findData.FirstMidName = obj.FirstMidName;
                findData.LastName = obj.LastName;
                findData.EnrollmentDate = obj.EnrollmentDate;
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
