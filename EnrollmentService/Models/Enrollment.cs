using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Models
{
    public enum Grade 
    { 
        A, B, C, D, E, F
    }

    public class Enrollment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public Grade? Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
