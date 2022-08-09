using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tugas2BE.Domain;

namespace Tugas2BE.Data.Interface
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> ByFirstName(string Fname);
        Task<IEnumerable<Student>> ByLastName(string Lname);
        Task<IEnumerable<Student>> StudentWithCourse();

    }
}
