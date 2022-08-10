using AutoMapper;

using Tugas2BE.DTO;
using Tugas2BE.Models;

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
