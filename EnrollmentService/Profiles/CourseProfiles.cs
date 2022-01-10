using AutoMapper;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
    public class CourseProfiles : Profile
    {
        public CourseProfiles()
        {
            CreateMap<Course, CourseView>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => src.Credit * 1.5));
            CreateMap<CreateCourseDto, Course>();
        }
    }
}
