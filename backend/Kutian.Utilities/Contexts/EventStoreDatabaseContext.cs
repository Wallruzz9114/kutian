using System;
using System.Collections.Generic;
using System.Linq;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kutian.Utilities.Contexts
{
    public class EventStoreDatabaseContext : DbContext, IEventStoreDatabaseContext
    {
        public EventStoreDatabaseContext(DbContextOptions options) : base(options) { }

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<StoredEvent> StoredEvents { get; private set; }
        public DbSet<Snapshot> SnapShots { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var valueComparer = new ValueComparer<IDictionary<string, HashSet<AggregateRoot>>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode()))
            );

            modelBuilder.Entity<Snapshot>()
                .Property(o => o.Data).HasConversion(
                    data => JsonConvert.SerializeObject(data),
                    data => JsonConvert.DeserializeObject<Dictionary<string, HashSet<AggregateRoot>>>(data)
                )
                .Metadata.SetValueComparer(valueComparer);

            base.OnModelCreating(modelBuilder);
        }
    }
}