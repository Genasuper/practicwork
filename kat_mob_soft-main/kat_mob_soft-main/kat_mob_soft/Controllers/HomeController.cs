using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using kat_mob_soft.DAL;
using System;
using kat_mob_soft.ViewModels.LoginAndRegistration; // ДОБАВЬТЕ ЭТУ СТРОКУ

namespace kat_mob_soft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Главная страница — сразу SiteInformation
        public IActionResult Index()
        {
            return RedirectToAction("SiteInformation");
        }

        public IActionResult SiteInformation()
        {
            // Проверка подключения к БД
            try
            {
                // Пытаемся выполнить простой запрос
                var canConnect = _context.Database.CanConnect();
                ViewBag.DbStatus = canConnect ? "База данных подключена!" : "Нет подключения к БД";

                // Или посчитать пользователей
                var usersCount = _context.Users.Count();
                ViewBag.UsersCount = usersCount;
            }
            catch (Exception ex)
            {
                ViewBag.DbStatus = $"Ошибка подключения: {ex.Message}";
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        // GET: /Home/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Возвращаем ошибки валидации
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, errors });
            }

            // TODO: Добавить вызов сервиса для аутентификации
            // var result = await _authService.AuthenticateAsync(model);

            _logger.LogInformation($"Login attempt for user: {model.Email}");

            return Json(new
            {
                success = true,
                message = "Вход выполнен успешно!",
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        // GET: /Home/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, errors });
            }

            // TODO: Добавить логику регистрации
            // var result = await _authService.RegisterAsync(model);

            _logger.LogInformation($"Registration attempt for user: {model.Email}");

            return Json(new
            {
                success = true,
                message = "Регистрация прошла успешно!",
                redirectUrl = Url.Action("Login", "Home")
            });
        }

        // POST: /Home/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // TODO: Добавить логику выхода
            _logger.LogInformation("User logged out");

            return Json(new
            {
                success = true,
                message = "Выход выполнен успешно!",
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}