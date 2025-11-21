using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.Domain.ModelsDb;
using kat_mob_soft.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace kat_mob_soft.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageStorage _contactMessageStorage;

        public ContactController(IContactMessageStorage contactMessageStorage)
        {
            _contactMessageStorage = contactMessageStorage;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 🔥 ПРОСТОЙ ТЕСТ - ДОЛЖЕН РАБОТАТЬ 100%
        [HttpGet]
        public IActionResult Test()
        {
            return Content("🎯 ТЕСТ ПРОЙДЕН! ContactController работает!");
        }

        // 🔥 ТЕСТ ХРАНИЛИЩА - ПОШАГОВАЯ ПРОВЕРКА
        [HttpGet]
        public async Task<IActionResult> TestStorage()
        {
            try
            {
                // ШАГ 1: Проверяем что хранилище не null
                if (_contactMessageStorage == null)
                {
                    return Content("❌ ШАГ 1: Хранилище равно NULL! Проверьте DI регистрацию.");
                }

                // ШАГ 2: Пробуем создать сообщение
                var testMessage = new ContactMessageDb
                {
                    Name = "Test User",
                    Email = "test@example.com",
                    Subject = "Test Storage",
                    Message = "Testing the new storage implementation",
                    Status = "new",
                    CreatedAt = DateTime.Now
                };

                // ШАГ 3: Пробуем сохранить в БД
                int id = await _contactMessageStorage.CreateAsync(testMessage);

                return Content($"✅ ВСЁ РАБОТАЕТ! Сообщение сохранено с ID: {id}");
            }
            catch (Exception ex)
            {
                return Content($"❌ Ошибка: {ex.Message}<br>Тип: {ex.GetType().Name}");
            }
        }

        // 🔥 МЕТОД ДЛЯ ОБРАБОТКИ ФОРМЫ - ИСПРАВЛЕННАЯ ВЕРСИЯ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactMessageViewModel model)
        {
            Console.WriteLine($"🔄 SendMessage вызван! Данные: {model?.Name}, {model?.Email}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ Модель не валидна!");
                TempData["ErrorMessage"] = "Пожалуйста, заполните все поля правильно";
                return RedirectToAction("Contacts", "Home");
            }

            try
            {
                Console.WriteLine("✅ Модель валидна, сохраняем в БД...");

                var messageDb = new ContactMessageDb
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message,
                    Status = "new",
                    CreatedAt = DateTime.Now
                };

                int messageId = await _contactMessageStorage.CreateAsync(messageDb);

                Console.WriteLine($"✅ Сообщение сохранено с ID: {messageId}");

                TempData["SuccessMessage"] = "Ваше сообщение успешно отправлено! Мы ответим в течение 24 часов.";
                return RedirectToAction("Contacts", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
                TempData["ErrorMessage"] = "Произошла ошибка при отправке сообщения. Пожалуйста, попробуйте еще раз.";
                return RedirectToAction("Contacts", "Home");
            }
        }
    }
}