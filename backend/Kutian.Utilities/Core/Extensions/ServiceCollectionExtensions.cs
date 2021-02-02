using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Models;
using Kutian.Utilities.Contexts;
using Kutian.Utilities.Store.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kutian.Utilities.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEventStore(this IServiceCollection services, EventStoreBuilderOptions eventStoreBuilderOptions)
        {
            services.AddTransient<IEventStoreDatabaseContext, EventStoreDatabaseContext>();
            services.AddTransient<IAggregateSet, AggregateSet>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddDbContext<EventStoreDatabaseContext>(options =>
            {
                options.UseNpgsql(
                    eventStoreBuilderOptions.ConnectionString,
                    builder => builder
                        .MigrationsAssembly(eventStoreBuilderOptions.MigrationAssembly)
                        .EnableRetryOnFailure()
                )
                .UseLoggerFactory(EventStoreDatabaseContext.ConsoleLoggerFactory);
            });
        }
    }
}