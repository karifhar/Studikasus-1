using EnrollmentService.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.ValidateAttributes
{
    public class TheIdmustgreaterthan : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var enroll = (CreateEnrollDto)validationContext.ObjectInstance;
            if (enroll.StudentId <= 0 && enroll.CourseId <= 0)
            {
                return new ValidationResult("Isi sesuai Id student dan id course", new[] { "Validation" });
            }
            return ValidationResult.Success;
        }
    }
}
