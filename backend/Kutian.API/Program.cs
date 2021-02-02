using System;
using System.Linq;
using Kutian.Application.Seed;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Utils;
using Kutian.Utilities.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kutian.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            ProcessDatabase(args, host);

            host.Run();
        }

        private static void ProcessDatabase(string[] args, IHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using var scope = services.CreateScope();
            var eventStoreDbContext = scope.ServiceProvider.GetRequiredService<EventStoreDatabaseContext>();
            var databaseContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            if (args.Contains("ci"))
                args = new string[4] { "dropdb", "migratedb", "seeddb", "stop" };

            if (args.Contains("dropdb")) eventStoreDbContext.Database.EnsureDeleted();
            if (args.Contains("migratedb")) eventStoreDbContext.Database.Migrate();

            if (args.Contains("seeddb"))
            {
                eventStoreDbContext.Database.EnsureCreated();
                DataSeeder.Seed(databaseContext);
            }

            if (args.Contains("secret"))
            {
                Console.WriteLine(SecretGenerator.GenerateSecret());
                Environment.Exit(0);
            }

            if (args.Contains("stop"))
                Environment.Exit(0);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
