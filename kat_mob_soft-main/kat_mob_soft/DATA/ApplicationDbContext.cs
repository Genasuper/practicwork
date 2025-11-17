using Microsoft.EntityFrameworkCore;
using PraticProect.Models;

namespace PraticProect.DATA // ← namespace должен быть PraticProect.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }
    }
}