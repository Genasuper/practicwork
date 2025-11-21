using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace kat_mob_soft.Domain.ModelsDb
{
    public class UserProfileDb
    {
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Навигационное свойство
        public virtual UserDb User { get; set; }
    }
}