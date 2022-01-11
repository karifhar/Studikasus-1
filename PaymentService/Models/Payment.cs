using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int EnrollmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public float TotalPrice { get; set; }
    }
}
