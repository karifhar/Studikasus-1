using EnrollmentService.Data.Database;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class EnrollmentRepo : IEnrollment
    {
        private AppDbContext _db;

        public EnrollmentRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task Delete(int id)
        {
            try
            {
                var data = await _db.Enrollments.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    _db.Enrollments.Remove(data);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var enrollments = await _db.Enrollments.Include(e => e.Student)
                .Include(e => e.Course).AsNoTracking().ToListAsync();

            return enrollments;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var enroll = await _db.Enrollments.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (enroll == null)
            {
                throw new Exception($" Data denga id : {id} tidak ditemukan");
            }
            return enroll;
        }

        public async Task<Enrollment> Insert(Enrollment entity)
        {
            try
            {
                _db.Enrollments.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Enrollment> Update(int id, Enrollment entity)
        {
            try
            {
                var data = await _db.Enrollments.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.StudentId = entity.StudentId;
                    data.CourseId = entity.CourseId;
                    _db.Enrollments.Update(data);
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
