using Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using PraticProect.Models;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserDb> Users { get; set; }
        public DbSet<CategoryDb> Categories { get; set; }
        public DbSet<EquipmentDb> Equipment { get; set; }
        public DbSet<EquipmentImageDb> EquipmentImages { get; set; }
        public DbSet<RentalOrderDb> RentalOrders { get; set; }
        public DbSet<RentalRequestDb> RentalRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Категория -> Оборудование
            modelBuilder.Entity<EquipmentDb>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Equipment)
                .HasForeignKey(e => e.CategoryId);

            // Оборудование -> Изображения
            modelBuilder.Entity<EquipmentImageDb>()
                .HasOne(i => i.Equipment)
                .WithMany(e => e.Images)
                .HasForeignKey(i => i.EquipmentId);

            // Пользователь -> Заказы
            modelBuilder.Entity<RentalOrderDb>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // Оборудование -> Заказы
            modelBuilder.Entity<RentalOrderDb>()
                .HasOne(o => o.Equipment)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EquipmentId);

            // Пользователь -> Запросы
            modelBuilder.Entity<RentalRequestDb>()
                .HasOne(r => r.User)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.UserId);
        }
    }
}