using AutoMapper;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
    public class EnrollmentProfiles : Profile
    {
        public EnrollmentProfiles()
        {
            CreateMap<CreateEnrollDto, Enrollment>();
        }
    }
}
