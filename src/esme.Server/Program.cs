using esme.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace esme.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Migrations");
                logger.LogInformation("Start upgrading database.");
                try
                {
                    // apply all outstanding migrations:
                    var db = services.GetService<ApplicationDbContext>();
                    db.Database.SetCommandTimeout(600);
                    db.Database.Migrate();
                    db.Database.SetCommandTimeout(30);
                }
                catch (Exception x)
                {
                    logger.LogError($"Error occured during migrations: {x.Message}, {x.InnerException} {x.InnerException?.Message}");
                    throw;
                }
                finally
                {
                    logger.LogInformation("Finished updgrading database.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config.SetBasePath(context.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json");
                    config.AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional: true);
                    config.AddUserSecrets<Startup>();
                })
                .UseStartup<Startup>()
                .Build();
    }
}
