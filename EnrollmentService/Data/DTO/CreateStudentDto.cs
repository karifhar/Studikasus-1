using System;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Data.DTO
{
    public class CreateStudentDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
