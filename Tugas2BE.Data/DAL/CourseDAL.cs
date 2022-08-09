using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tugas2BE.Data.Interface;
using Tugas2BE.Domain;

namespace Tugas2BE.Data.DAL
{
    public class CourseDAL : ICourse
    {
        private readonly DataContext _context;

        public CourseDAL(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<IEnumerable<Course>> ByTitle(string title)
        {
            var results = await _context.Courses.Where(c => c.Title.Contains(title)).ToListAsync();
            if (results == null) throw new Exception($"Title Yang Mengandung  kata {title} Tidak Ditemukan");

            return results;
        }

        public async Task<IEnumerable<Course>> CourseByStudent()
        {
            var results = await _context.Courses.Include(c => c.Enrollments).ThenInclude(e => e.Student).ToListAsync();
            return results;
        }

        public async Task DeleteById(int id)
        {
            var findData = await _context.Courses.SingleOrDefaultAsync(c=> c.CourseID == id);
            if (findData == null) throw new Exception($"Data dengan ID {id} Tidak Ditemukan");

            _context.Remove(findData);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _context.Courses.OrderBy(c => c.Title).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(int id)
        {
            var result = await _context.Courses.SingleOrDefaultAsync(s => s.CourseID == id);
            if (result == null) throw new Exception($"Data dengan ID {id} Tidak Ditemukan");

            return result;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _context.Courses.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Course> Update(Course obj)
        {
            try
            {
                var findData = await _context.Courses.SingleOrDefaultAsync(s => s.CourseID == obj.CourseID);
                if (findData == null) throw new Exception($"Data dengan ID {obj.CourseID} Tidak Ditemukan");

                findData.Title = obj.Title;
                findData.Credits = obj.Credits;
                
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
