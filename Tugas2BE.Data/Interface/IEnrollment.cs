using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tugas2BE.Domain;

namespace Tugas2BE.Data.Interface
{
    public interface IEnrollment : ICrud<Enrollment>
    {
        //Task<IEnumerable<Course>> ByCourse(string Course);
    }
}
