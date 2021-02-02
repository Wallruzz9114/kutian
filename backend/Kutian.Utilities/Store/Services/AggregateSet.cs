using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Store.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Kutian.Utilities.Store.Services
{
    public class AggregateSet : IAggregateSet
    {
        private readonly IEventStoreDatabaseContext _eventDbContext;
        private readonly IDateTime _dateTime;

        public AggregateSet(IEventStoreDatabaseContext eventDbContext, IDateTime dateTime)
        {
            _eventDbContext = eventDbContext;
            _dateTime = dateTime;
        }

        public IQueryable<TAggregateRoot> Set<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            var aggregates = new List<TAggregateRoot>();
            var aggregateName = typeof(TAggregateRoot).Name;
            var storedEventsGroups = from storedEvent in GetStoredEvents(aggregateName)
                                     group storedEvent by storedEvent.StreamId into storedEventsGroup
                                     orderby storedEventsGroup.Key
                                     select storedEventsGroup;

            foreach (var storedEventGroup in storedEventsGroups)
            {
                var aggregate = (TAggregateRoot)FormatterServices
                    .GetSafeUninitializedObject(typeof(TAggregateRoot));

                foreach (var storedEvent in storedEventGroup.OrderBy(x => x.CreatedAt))
                    aggregate.Apply(
                        JsonConvert.DeserializeObject(
                            storedEvent.Data, Type.GetType(storedEvent.DotNetType)));

                aggregates.Add(aggregate);
            }

            return aggregates.AsQueryable();
        }

        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            var aggregate = (TAggregateRoot)FormatterServices.GetUninitializedObject(typeof(TAggregateRoot));
            var storedEvents = await GetStoredEvents(
                typeof(TAggregateRoot).Name,
                new Guid[1] { id }
            ).ToListAsync();

            foreach (var storedEvent in storedEvents.OrderBy(x => x.CreatedAt))
                aggregate.Apply(
                    JsonConvert.DeserializeObject(
                        storedEvent.Data, Type.GetType(storedEvent.DotNetType)));

            return aggregate;
        }

        private IQueryable<StoredEvent> GetStoredEvents(string aggregate, Guid[] streamIds = null, DateTime? createdAt = null)
        {
            createdAt ??= _dateTime.UtcNow;

            var storedEvents = _eventDbContext.StoredEvents
                .Where(x => x.Aggregate == aggregate)
                .ToList();

            var idsFromEvents = new List<Guid>();

            foreach (var storedEvent in storedEvents)
                idsFromEvents.Add(storedEvent.StreamId);

            var ids = streamIds ?? idsFromEvents.ToArray();

            return from storedEvent in _eventDbContext.StoredEvents
                   where ids.Contains(storedEvent.StreamId) && storedEvent.CreatedAt <= createdAt
                   select storedEvent;
        }
    }
}