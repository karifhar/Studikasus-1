using System.ComponentModel.DataAnnotations;

namespace PaymentService.Data.Dtos
{
    public class CreatePaymentDto
    {
        [Required]
        public int EnrollmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
       
    }
}
