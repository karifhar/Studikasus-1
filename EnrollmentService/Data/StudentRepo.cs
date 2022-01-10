using EnrollmentService.Data.Database;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class StudentRepo : IStudent
    {
        private AppDbContext _db;

        public StudentRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Delete(int id)
        {
            try
            {
                var data = await _db.Students.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    _db.Students.Remove(data);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var students = await _db.Students.OrderBy(e => e.FullName).ToListAsync();
            return students;
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _db.Students.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new Exception($" Data denga id : {id} tidak ditemukan");
            }
            return student;
        }

        public async Task<Student> Insert(Student entity)
        {
            try
            {
                 _db.Students.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> Update(int id, Student entity)
        {
            try
            {
                var data = await _db.Students.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.FullName = entity.FullName;
                    data.EnrollmentDate = entity.EnrollmentDate;
                    _db.Students.Update(data);
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
