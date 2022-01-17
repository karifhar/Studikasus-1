using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data.Database;
using PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Data
{
    public class PaymentRepo : IPayment
    {
        private ApplicationDbContext _db;

        public PaymentRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Delete(int id)
        {
            try
            {
                var data = await _db.Payments.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    _db.Payments.Remove(data);
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
            var payments = await _db.Payments.OrderBy(e => e.Id).ToListAsync();
            return payments;
        }

        public async Task<Enrollment> GetById(int id)
        {
            var payment = await _db.Payments.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (payment == null)
            {
                throw new Exception($" Data denga id : {id} tidak ditemukan");
            }
            return payment;
        }

        public async Task<Enrollment> Insert(Enrollment entity)
        {
            try
            {
                entity.TotalPrice = 50000;
                _db.Payments.Add(entity);
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
                var data = await _db.Payments.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.StudentId = entity.StudentId;
                    data.CourseId = entity.CourseId;
                    _db.Payments.Update(data);
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
