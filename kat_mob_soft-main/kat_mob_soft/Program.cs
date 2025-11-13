using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PraticProect.Data;
using PraticProect.Services;

var builder = WebApplication.CreateBuilder(args);

// Регистрация DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервиса
builder.Services.AddScoped<EquipmentService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Автомиграции
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
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