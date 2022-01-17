using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float? TotalPrice { get; set; }

    }
}
