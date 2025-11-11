using System;

namespace PraticProect.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Equipment? Equipment { get; set; }
    }
}