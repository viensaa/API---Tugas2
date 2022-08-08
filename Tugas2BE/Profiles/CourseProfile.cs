using AutoMapper;
using Tugas2BE.Domain;
using Tugas2BE.DTO;

namespace Tugas2BE.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO,Course>();
            CreateMap<CourseCreateDTO,Course>();
        }
    }
}
