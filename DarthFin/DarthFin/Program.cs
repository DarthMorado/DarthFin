using DarthFin.DB;
using DarthFin.DB.Repositories;
using DarthFin.Mapping;
using DarthFin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

namespace DarthFin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

            ConfigureServices(builder.Services, builder.Configuration);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(4200); // HTTP
                options.ListenAnyIP(4201, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            ConfigureDatabase(services, config);

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
        }

        public static void ConfigureDatabase(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<Database>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFilesRepository, FilesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();

        }
    }
}
