using Microsoft.AspNetCore.Mvc;
using PraticProect.Models;
using PraticProect.DATA;
using System.Linq;
using System.Threading.Tasks;

namespace PraticProect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Проверяем валидацию
                if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new { success = false, message = "Все поля обязательны для заполнения" });
                }

                if (request.Password.Length < 6)
                {
                    return BadRequest(new { success = false, message = "Пароль должен содержать минимум 6 символов" });
                }

                // Проверяем существование пользователя
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
                if (existingUser != null)
                {
                    return Conflict(new { success = false, message = "Пользователь с таким email уже существует" });
                }

                // Создаем нового пользователя
                var user = new User
                {
                    Login = request.Name,
                    Email = request.Email,
                    Password = request.Password, // В реальном приложении хешируйте пароль!
                    Role = "user",
                    CreatedAt = System.DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Регистрация прошла успешно! Теперь вы можете войти в систему.",
                    user = new
                    {
                        id = user.Id,
                        name = user.Login,
                        email = user.Email
                    }
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Ошибка сервера при регистрации" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Проверяем валидацию
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new { success = false, message = "Email и пароль обязательны" });
                }

                // Ищем пользователя
                var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
                if (user == null)
                {
                    return Unauthorized(new { success = false, message = "Пользователь с таким email не найден" });
                }

                // Проверяем пароль
                if (user.Password != request.Password)
                {
                    return Unauthorized(new { success = false, message = "Неверный пароль" });
                }

                // Успешный вход
                return Ok(new
                {
                    success = true,
                    message = "Вход выполнен успешно!",
                    user = new
                    {
                        id = user.Id,
                        name = user.Login,
                        email = user.Email,
                        role = user.Role
                    }
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Ошибка сервера при входе" });
            }
        }

        // Дополнительный endpoint для проверки существования email
        [HttpGet("check-email/{email}")]
        public IActionResult CheckEmail(string email)
        {
            var exists = _context.Users.Any(u => u.Email == email);
            return Ok(new { exists = exists });
        }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}