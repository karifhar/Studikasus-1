using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public  DbSet<Payment> Payments { get; set; }
    }
}
