using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kat_mob_soft.Domain.ModelsDb
{
    public class UserDb
    {
        public int Id { get; set; }

        [Required]
        [Column("login")]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [Column("password")]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("role")]
        [StringLength(20)]
        public string Role { get; set; } = "user";

        [Column("avatar_path")]
        public string AvatarPath { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual UserProfileDb Profile { get; set; }
        public virtual ICollection<OrderDb> Orders { get; set; }

        // Вычисляемые свойства (не сохраняются в БД)
        [NotMapped]
        public string FirstName { get; set; }

        [NotMapped]
        public string LastName { get; set; }
    }
}