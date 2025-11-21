using kat_mob_soft.Service;
using kat_mob_soft.ViewModels.LoginAndRegistration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace kat_mob_soft.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            Console.WriteLine("=== GET Register вызван ===");
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Console.WriteLine($"=== КОНТРОЛЛЕР: Начало регистрации ===");
            Console.WriteLine($"Email: {model?.Email}, Имя: {model?.FirstName}, Фамилия: {model?.LastName}");

            if (ModelState.IsValid)
            {
                Console.WriteLine("✅ ModelState валиден");

                var result = await _userService.RegisterUserAsync(
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    model.Password
                );

                Console.WriteLine($"Результат из UserService: {result}");

                if (result)
                {
                    Console.WriteLine("🎉 РЕГИСТРАЦИЯ УСПЕШНА!");

                    // ВАРИАНТ 1: Редирект на Login с сообщением
                    TempData["SuccessMessage"] = "Регистрация прошла успешно! Теперь вы можете войти.";
                    Console.WriteLine($"TempData установлен: {TempData["SuccessMessage"]}");

                    // ВАРИАНТ 2: Остаемся на странице регистрации с сообщением (раскомментируйте если вариант 1 не работает)
                    // ViewBag.SuccessMessage = "Регистрация прошла успешно! Теперь вы можете войти.";
                    // return View(new RegisterViewModel()); // Возвращаем пустую модель

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    Console.WriteLine("❌ РЕГИСТРАЦИЯ НЕ УДАЛАСЯ - ПОЛЬЗОВАТЕЛЬ УЖЕ СУЩЕСТВУЕТ");
                    ModelState.AddModelError("", "Пользователь с таким email уже существует.");
                }
            }
            else
            {
                Console.WriteLine("❌ ModelState невалиден:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Ошибка валидации: {error.ErrorMessage}");
                }
            }

            Console.WriteLine("🔄 ВОЗВРАТ НА СТРАНИЦУ РЕГИСТРАЦИИ");
            return View(model);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            Console.WriteLine("=== GET Login вызван ===");

            // Проверим, есть ли сообщение об успехе
            if (TempData["SuccessMessage"] != null)
            {
                var message = TempData["SuccessMessage"].ToString();
                Console.WriteLine($"✅ Сообщение в TempData: {message}");
                ViewBag.SuccessMessage = message;
            }
            else
            {
                Console.WriteLine("❌ Сообщения в TempData нет");
            }

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine($"=== Вход: {model?.Email} ===");

            if (ModelState.IsValid)
            {
                var isValid = await _userService.ValidateUserAsync(model.Email, model.Password);
                if (isValid)
                {
                    Console.WriteLine("✅ Вход успешен - редирект на главную");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Console.WriteLine("❌ Неверный email или пароль");
                    ModelState.AddModelError("", "Неверный email или пароль.");
                }
            }
            else
            {
                Console.WriteLine("❌ ModelState невалиден при входе");
            }

            return View(model);
        }

        // Тестовый метод для проверки
        public IActionResult DebugTest()
        {
            Console.WriteLine("=== DEBUG TEST ===");
            TempData["TestMessage"] = "Test message works!";
            return RedirectToAction("Login");
        }
    }
}