using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Data.DTO
{
    public enum Grade
    {
        A, B, C, D, E, F
    }

    public class EnrollReadDto
    {
        public int? Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public Grade? Grade { get; set; }
    }
}
