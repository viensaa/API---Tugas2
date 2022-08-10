using AutoMapper;

using Tugas2BE.DTO;
using Tugas2BE.Models;

namespace Tugas2BE.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();            
            CreateMap<StudentCreateDTO,Student>();
          
            CreateMap<Student, StudentWithCourseDTO>();                      
            CreateMap<Enrollment, CourseDTO>();

            
            

        }
    }
}
