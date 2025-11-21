using kat_mob_soft.Domain.Models;
using kat_mob_soft.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;

namespace kat_mob_soft.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDb> Users { get; set; }
        public DbSet<GiftCertificateDb> GiftCertificates { get; set; }
        public DbSet<OrderDb> Orders { get; set; }
        public DbSet<UserProfileDb> UserProfiles { get; set; }
        public DbSet<ContactMessageDb> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка имен таблиц под вашу существующую БД
            modelBuilder.Entity<UserDb>().ToTable("users");
            modelBuilder.Entity<GiftCertificateDb>().ToTable("gift_certificates");
            modelBuilder.Entity<OrderDb>().ToTable("orders");
            modelBuilder.Entity<UserProfileDb>().ToTable("user_profiles");
            modelBuilder.Entity<ContactMessageDb>().ToTable("contact_messages");

            // Настройка UserDb для регистрации
            modelBuilder.Entity<UserDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Login)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnName("login");

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("email");

                // Уникальный индекс для email
                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasColumnName("password");

                entity.Property(e => e.Role)
                      .HasMaxLength(20)
                      .HasColumnName("role")
                      .HasDefaultValue("user");

                entity.Property(e => e.AvatarPath)
                      .HasColumnName("avatar_path");

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Настройка UserProfileDb
            modelBuilder.Entity<UserProfileDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.UserId)
                      .HasColumnName("user_id");

                entity.Property(e => e.FullName)
                      .HasColumnName("full_name")
                      .HasMaxLength(200);

                entity.Property(e => e.BirthDate)
                      .HasColumnName("birth_date");

                entity.Property(e => e.PhoneNumber)
                      .HasMaxLength(20)
                      .HasColumnName("phone_number");

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Настройка GiftCertificateDb
            modelBuilder.Entity<GiftCertificateDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Title)
                      .HasColumnName("title")
                      .HasMaxLength(255);

                entity.Property(e => e.Description)
                      .HasColumnName("description");

                entity.Property(e => e.Price)
                      .HasColumnName("price")
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ImagePath)
                      .HasColumnName("image_path");

                entity.Property(e => e.IsActive)
                      .HasColumnName("is_active")
                      .HasDefaultValue(true);

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Настройка OrderDb
            modelBuilder.Entity<OrderDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.UserId)
                      .HasColumnName("user_id");

                entity.Property(e => e.CertificateId)
                      .HasColumnName("certificate_id");

                entity.Property(e => e.RecipientName)
                      .HasColumnName("recipient_name")
                      .HasMaxLength(255);

                entity.Property(e => e.RecipientEmail)
                      .HasColumnName("recipient_email")
                      .HasMaxLength(255);

                entity.Property(e => e.GiftMessage)
                      .HasColumnName("gift_message");

                entity.Property(e => e.AmountPaid)
                      .HasColumnName("amount_paid")
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .HasMaxLength(50)
                      .HasDefaultValue("pending");

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // НАСТРОЙКА ContactMessageDb - ОБНОВЛЕННАЯ
            modelBuilder.Entity<ContactMessageDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.UserId)
                      .HasColumnName("user_id");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("name");

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("email");

                entity.Property(e => e.Subject)
                      .IsRequired()
                      .HasMaxLength(500)
                      .HasColumnName("subject");

                entity.Property(e => e.Message)
                      .IsRequired()
                      .HasColumnName("message");

                entity.Property(e => e.Status)
                      .HasMaxLength(50)
                      .HasColumnName("status")
                      .HasDefaultValue("new");

                entity.Property(e => e.AdminNotes)
                      .HasColumnName("admin_notes");

                entity.Property(e => e.AdminId)
                      .HasColumnName("admin_id");

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                      .HasColumnName("updated_at");

                // Внешние ключи - делаем их опциональными
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Admin)
                      .WithMany()
                      .HasForeignKey(e => e.AdminId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Настройка связей (внешних ключей)
            modelBuilder.Entity<OrderDb>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderDb>()
                .HasOne(o => o.GiftCertificate)
                .WithMany(g => g.Orders)
                .HasForeignKey(o => o.CertificateId);

            modelBuilder.Entity<UserProfileDb>()
                .HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfileDb>(up => up.UserId);
        }
    }
}