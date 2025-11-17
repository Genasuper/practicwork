using Microsoft.EntityFrameworkCore;
using PraticProect.DATA;
using PraticProect.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер
builder.Services.AddControllersWithViews();

// Используем InMemory базу
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("PhotoRental"));

var app = builder.Build();

// Инициализируем тестовые данные
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Equipment.Any())
    {
        context.Equipment.AddRange(
            new Equipment { Id = 1, Name = "Canon EOS R5", Description = "Профессиональная камера", Category = "Камеры", PricePerDay = 2500, IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1502920917128-1aa500764cbd?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80" },
            new Equipment { Id = 2, Name = "Nikon Z7 II", Description = "Полнокадровая камера", Category = "Камеры", PricePerDay = 2200, IsAvailable = true, ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?ixlib=rb-4.0.3&auto=format&fit=crop&w=1000&q=80" }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline
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