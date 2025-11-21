using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kat_mob_soft.Domain.ModelsDb
{
    public class OrderDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CertificateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RecipientName { get; set; }

        [Required]
        [MaxLength(100)]
        public string RecipientEmail { get; set; }

        public string GiftMessage { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual UserDb User { get; set; }

        [ForeignKey("CertificateId")]
        public virtual GiftCertificateDb GiftCertificate { get; set; }
    }
}