using Microsoft.EntityFrameworkCore;
using PraticProect.DATA;
using PraticProect.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("PhotoRental"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Добавляем оборудование
    if (!context.Equipment.Any())
    {
        context.Equipment.AddRange(
            new Equipment
            {
                Id = 1,
                Name = "Canon EOS R5",
                Description = "Профессиональная беззеркальная камера с высоким разрешением",
                Category = "Камеры",
                PricePerDay = 2500,
                IsAvailable = true,
                ImageUrl = "https://images.unsplash.com/photo-1502920917128-1aa500764cbd?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
                Brand = "Canon",
                Model = "EOS R5"
            },
            new Equipment
            {
                Id = 2,
                Name = "Nikon Z7 II",
                Description = "Полнокадровая беззеркальная камера для профессионалов",
                Category = "Камеры",
                PricePerDay = 2200,
                IsAvailable = true,
                ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
                Brand = "Nikon",
                Model = "Z7 II"
            }
        );
        context.SaveChanges();
    }

    // Добавляем тестовых пользователей
    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User
            {
                Id = 1,
                Login = "Иван Петров",
                Email = "ivan@example.com",
                Password = "password123",
                Role = "user",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = 2,
                Login = "Администратор",
                Email = "admin@example.com",
                Password = "admin123",
                Role = "admin",
                CreatedAt = DateTime.UtcNow
            }
        );
        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();