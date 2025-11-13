using System.ComponentModel.DataAnnotations;

namespace PhotoRental.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}