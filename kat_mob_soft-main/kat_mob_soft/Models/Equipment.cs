using System.ComponentModel.DataAnnotations;

namespace PraticProect.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        public decimal PricePerDay { get; set; }

        public string Category { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Brand { get; set; }
        public string? Model { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}