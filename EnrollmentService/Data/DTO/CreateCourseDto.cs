using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Data.DTO
{
    public class CreateCourseDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Credit { get; set; }
    }
}
