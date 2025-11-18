using Microsoft.AspNetCore.Mvc;
using PraticProect.Models;
using PraticProect.DATA;
using System.Linq;
using System.Threading.Tasks;

namespace PraticProect.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                // Проверяем существование пользователя
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    return BadRequest("Пользователь с таким email уже существует");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Регистрация успешна" });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Ошибка сервера");
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null || user.Password != model.Password) // Используем Password
                {
                    return Unauthorized("Неверный email или пароль");
                }

                return Ok(new
                {
                    message = "Вход выполнен",
                    user = new
                    {
                        id = user.Id,
                        login = user.Login, // Используем Login
                        email = user.Email
                    }
                });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Ошибка сервера");
            }
        }
    }

    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}