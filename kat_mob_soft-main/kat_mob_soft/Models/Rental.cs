using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoRental.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ID оборудования обязателен")]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "ID пользователя обязателен")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Дата аренды обязательна")]
        public DateTime RentalDate { get; set; }

        [Required(ErrorMessage = "Дата возврата обязательна")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Общая цена обязательна")]
        [Range(0.01, 100000, ErrorMessage = "Цена должна быть от 0.01 до 100000")]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Active"; // Active, Completed, Cancelled

        // Навигационные свойства
        public Equipment Equipment { get; set; }
        public User User { get; set; }
    }
}