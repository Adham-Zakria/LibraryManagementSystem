using BusinessLogic.Profiles;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Contexts;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Configuration;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new LibraryProfile()));

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(redisConnection!);
            });

            builder.Services.AddScoped<IBookLibraryRepository, BookLibraryRepository>();
            builder.Services.AddScoped<IBookLibraryService, BookLibraryService>();


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
        }
    }
}
