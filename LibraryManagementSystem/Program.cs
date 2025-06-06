using BusinessLogic.Profiles;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Contexts;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using DataAccess.Seeding;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Configuration;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

            #endregion

            var app = builder.Build();

            await DbInitializer(app);

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

        public static async Task DbInitializer(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<LibraryDbContext>();

            await Seeder.SeedAsync(context);
        }
    }
}
