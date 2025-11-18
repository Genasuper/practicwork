using System.ComponentModel.DataAnnotations;

namespace PraticProect.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email адрес")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Дата начала аренды обязательна")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Дата окончания аренды обязательна")]
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Подтвержден";

        public Equipment Equipment { get; set; }
    }
}