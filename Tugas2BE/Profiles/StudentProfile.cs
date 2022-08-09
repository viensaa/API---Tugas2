using AutoMapper;
using Tugas2BE.Domain;
using Tugas2BE.DTO;

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

            
            CreateMap< CourseDTO, Enrollment>();
            CreateMap< Course, Student>();
            CreateMap< Course, StudentWithCourseDTO>();
            CreateMap< CourseDTO, StudentWithCourseDTO>();
            CreateMap< Enrollment, Course>();
            CreateMap<Course,Enrollment >();

        }
    }
}
