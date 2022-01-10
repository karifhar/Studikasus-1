using EnrollmentService.Models;
using System;
using System.Linq;

namespace EnrollmentService.Data.Database
{
    public static class DbInitializer
    {
        public static void Initializer(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student {FullName= "Tom Clark", EnrollmentDate= DateTime.Parse("2022-01-01")},
                new Student {FullName= "Tony Stark", EnrollmentDate=DateTime.Parse("2022-01-01")},
                new Student {FullName= "Natasya Willow", EnrollmentDate=DateTime.Parse("2022-01-01")}
            };

            foreach (var student in students)
            {
                context.Students.Add(student);
            }

            context.SaveChanges();

            var courses = new Course[]
            {
                new Course {Title = "NodeJS", Credit=2,},
                new Course {Title = "ASP .NET", Credit=2},
                new Course {Title = "React Native", Credit=3}
            };

            foreach (var c in courses)
            {
                context.Courses.Add(c);
            }

            context.SaveChanges();

            var enroll = new Enrollment[]
            {
                new Enrollment {StudentId=1, CourseId=1, Grade=Grade.A},
                new Enrollment {StudentId=2, CourseId=2, Grade= Grade.B},
                new Enrollment {StudentId=3, CourseId=3, Grade= Grade.C},
            };
            foreach (var enrollment in enroll)
            {
                context.Enrollments.Add(enrollment);
            }
            context.SaveChanges();
        }
    }
}
