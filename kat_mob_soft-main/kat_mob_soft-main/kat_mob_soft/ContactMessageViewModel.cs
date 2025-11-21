using System.ComponentModel.DataAnnotations;

namespace kat_mob_soft.ViewModels
{
    public class ContactMessageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Тема обязательна")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Сообщение обязательно")]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }

        public int? UserId { get; set; }
    }
}