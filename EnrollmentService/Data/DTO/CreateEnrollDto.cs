using EnrollmentService.Models;
using EnrollmentService.ValidateAttributes;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Data.DTO
{
    [TheIdmustgreaterthan]
    public class CreateEnrollDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public Grade? Grade { get; set; }
    }
}
