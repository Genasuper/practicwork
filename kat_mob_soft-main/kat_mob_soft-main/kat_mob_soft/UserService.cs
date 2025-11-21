using kat_mob_soft.DAL;
using kat_mob_soft.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace kat_mob_soft.Service
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string password);
        Task<UserDb> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserAsync(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string password)
        {
            try
            {
                Console.WriteLine($"=== НАЧАЛО РЕГИСТРАЦИИ ===");
                Console.WriteLine($"Email: {email}, Имя: {firstName}, Фамилия: {lastName}");

                // Проверяем, существует ли пользователь с таким email
                Console.WriteLine("Проверка существующего пользователя...");
                var existingUser = await _context.Users.AnyAsync(u => u.Email == email);
                if (existingUser)
                {
                    Console.WriteLine($"❌ Пользователь с email {email} уже существует");
                    return false;
                }
                Console.WriteLine("✅ Пользователь с таким email не найден");

                // Создаем пользователя
                Console.WriteLine("Создание пользователя...");
                var user = new UserDb
                {
                    Login = email,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = "user",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                Console.WriteLine("Сохранение пользователя в БД...");
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Пользователь сохранен с ID: {user.Id}");

                // Создаем профиль пользователя
                Console.WriteLine("Создание профиля пользователя...");
                var userProfile = new UserProfileDb
                {
                    UserId = user.Id,
                    FullName = $"{firstName} {lastName}",
                    CreatedAt = DateTime.UtcNow
                };

                _context.UserProfiles.Add(userProfile);
                Console.WriteLine("Сохранение профиля в БД...");
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ Профиль создан для пользователя ID: {user.Id}");

                Console.WriteLine($"=== РЕГИСТРАЦИЯ УСПЕШНА ===");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ОШИБКА ПРИ РЕГИСТРАЦИИ:");
                Console.WriteLine($"Сообщение: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                }

                return false;
            }
        }

        public async Task<UserDb> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}