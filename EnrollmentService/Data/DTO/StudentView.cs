using System;

namespace EnrollmentService.Data.DTO
{
    public class StudentView
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime EnrollmentDate { get; set;}
    }
}
