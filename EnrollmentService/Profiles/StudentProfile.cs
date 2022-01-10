using AutoMapper;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentView>();
            CreateMap<CreateStudentDto, Student>();
        }
    }
}
