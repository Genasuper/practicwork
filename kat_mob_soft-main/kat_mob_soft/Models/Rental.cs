using System;

namespace PraticProect.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства для работы сайта
        public Equipment Equipment { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
    }
}