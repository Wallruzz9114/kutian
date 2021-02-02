using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Store.Models;
using Newtonsoft.Json;

namespace Kutian.Utilities.Store.Services
{
    public class EventStore : IEventStore
    {
        private readonly List<StoredEvent> _newEvents = new();
        private readonly IDateTime _dateTime;
        private readonly IEventStoreDatabaseContext _eventStoreDbContext;

        public EventStore(IDateTime dateTime, IEventStoreDatabaseContext eventStoreDbContext)
        {
            _dateTime = dateTime;
            _eventStoreDbContext = eventStoreDbContext;
        }

        public void Store(AggregateRoot aggregateRoot)
        {
            var aggregateType = aggregateRoot.GetType();
            var aggregateId = (Guid)aggregateType
                .GetProperty($"{ aggregateType.Name }Id")
                .GetValue(aggregateRoot, null);
            var aggregate = aggregateType.Name;

            foreach (var @event in aggregateRoot.DomainEvents)
            {
                var newEvent = new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = aggregate,
                    AggregateDotNetType = aggregateType.AssemblyQualifiedName,
                    Data = JsonConvert.SerializeObject(@event),
                    StreamId = aggregateId,
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedAt = _dateTime.UtcNow,
                    Sequence = 0
                };

                _newEvents.Add(newEvent);
            }

            aggregateRoot.ClearChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var @event in _newEvents)
                _eventStoreDbContext.StoredEvents.Add(@event);

            var result = await _eventStoreDbContext.SaveChangesAsync(cancellationToken);

            _newEvents.Clear();

            return result;
        }
    }
}