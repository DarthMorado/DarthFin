using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DarthTgEcho
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services, context.Configuration);
            });


#if DEBUG
            // Run as a console app in Debug mode
            builder.UseConsoleLifetime();
#else
// Run as a Windows Service in Release mode
builder.UseWindowsService();
#endif

            var host = builder.Build();
            host.Run();
        }

        private static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
        {
            services.AddHostedService<App>();
            services.Configure<EchoBotOptions>(configuration.GetSection(nameof(EchoBotOptions)));
            services.AddTransient<IEchoBot, EchoBot>();
        }
    }
}
