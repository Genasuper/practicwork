using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace kat_mob_soft.Domain.ModelsDb
{
    [Table("contact_messages")]
    public class ContactMessageDb
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("subject")]
        public string Subject { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("status")]
        public string Status { get; set; } = "new";

        [Column("admin_notes")]
        public string AdminNotes { get; set; }

        [Column("admin_id")]
        public int? AdminId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства
        [ForeignKey("UserId")]
        public virtual UserDb User { get; set; }

        [ForeignKey("AdminId")]
        public virtual UserDb Admin { get; set; }
    }
}