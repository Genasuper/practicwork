using System;

namespace kat_mob_soft.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CertificateId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string GiftMessage { get; set; }
        public decimal AmountPaid { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}