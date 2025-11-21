using System;

namespace kat_mob_soft.Domain.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}