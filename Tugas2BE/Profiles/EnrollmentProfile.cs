using AutoMapper;
using Tugas2BE.Domain;
using Tugas2BE.DTO;

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
