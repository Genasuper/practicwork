using kat_mob_soft.DAL;
using kat_mob_soft.DAL.Interfaces;
using kat_mob_soft.DAL.Storages;
using kat_mob_soft.Domain.Models;
using kat_mob_soft.Domain.ModelsDb;
using kat_mob_soft.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kat_mob_soft
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // База данных PostgreSQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Регистрация хранилищ
            services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
            services.AddScoped<IBaseStorage<GiftCertificateDb>, GiftCertificateStorage>();
            services.AddScoped<IBaseStorage<OrderDb>, OrderStorage>();
            services.AddScoped<IBaseStorage<UserProfileDb>, UserProfileStorage>();

            // НОВОЕ: Хранилище для контактных сообщений
            services.AddScoped<IContactMessageStorage, ContactMessageStorage>();

            // Добавьте эту строку для регистрации UserService
            services.AddScoped<IUserService, UserService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}