using AutoMapper;
using PaymentService.Data.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymentProfiiles : Profile
    {
        public PaymentProfiiles()
        {
            CreateMap<CreatePaymentDto, Payment>();
        }
    }
}
