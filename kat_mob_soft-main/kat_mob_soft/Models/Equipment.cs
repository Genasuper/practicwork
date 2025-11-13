using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoRental.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0.01, 10000, ErrorMessage = "Цена должна быть от 0.01 до 10000")]
        public decimal PricePerDay { get; set; }

        [StringLength(50, ErrorMessage = "Категория не должна превышать 50 символов")]
        public string Category { get; set; }

        public bool IsAvailable { get; set; } = true;
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}