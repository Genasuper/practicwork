using Microsoft.EntityFrameworkCore;
using PraticProect.Models;

namespace PraticProect.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Equipment)
                .WithMany(e => e.Rentals)
                .HasForeignKey(r => r.EquipmentId);
        }
    }
}