using Microsoft.EntityFrameworkCore;
using PhotoRental.Models;
using PraticProect.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PraticProect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}