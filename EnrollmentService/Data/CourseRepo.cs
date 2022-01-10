using EnrollmentService.Data.Database;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class CourseRepo : ICourse
    {
        private AppDbContext _db;

        public CourseRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Delete(int id)
        {
            try
            {
                var data = await _db.Courses.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    _db.Courses.Remove(data);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var courses = await _db.Courses.OrderBy(e => e.Title).ToListAsync();
            return courses;
        }

        public async Task<Course> GetById(int id)
        {
            var course = await _db.Courses.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                throw new Exception($" Data denga id : {id} tidak ditemukan");
            }
            return course;
        }

        public async Task<Course> Insert(Course entity)
        {
            try
            {
                _db.Courses.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Course> Update(int id, Course entity)
        {
            try
            {
                var data = await _db.Courses.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.Title = entity.Title;
                    data.Credit = entity.Credit;
                    _db.Courses.Update(data);
                }
                await _db.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
