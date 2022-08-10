using AutoMapper;

using Tugas2BE.DTO;
using Tugas2BE.Models;

namespace Tugas2BE.Profiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentDTO>();
            CreateMap<EnrollmentDTO,Enrollment>();
            CreateMap<EnrollmentCreateDTO,Enrollment>();
        }
    }
}
