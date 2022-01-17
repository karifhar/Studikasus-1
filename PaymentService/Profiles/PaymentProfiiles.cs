using AutoMapper;
using EnrollmentService.Data.DTO;
using PaymentService.Data.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymentProfiiles : Profile
    {
        public PaymentProfiiles()
        {
            CreateMap<EnrollReadDto, Enrollment>();
        }
    }
}
