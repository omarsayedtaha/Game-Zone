using Core_Layer.Interfaces;
using Game_Zone__PresentationLayer_.Helper;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;

namespace Game_Zone__PresentationLayer_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork)) ;
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            var app = builder.Build();

            using  var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;

            var dbContext = service.GetRequiredService<StoreContext>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger =loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message);
            }


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