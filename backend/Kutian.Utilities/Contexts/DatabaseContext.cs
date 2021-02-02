using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Utilities.Abstractions;

namespace Kutian.Utilities.Contexts
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateSet _aggregateSet;

        public DatabaseContext(IEventStore eventStore, IAggregateSet aggregateSet)
        {
            _aggregateSet = aggregateSet;
            _eventStore = eventStore;
        }

        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot =>
            await _aggregateSet.FindAsync<TAggregateRoot>(id);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
            await _eventStore.SaveChangesAsync(cancellationToken);

        public IQueryable<T> Set<T>() where T : AggregateRoot => _aggregateSet.Set<T>();

        public void Store(AggregateRoot aggregateRoot) => _eventStore.Store(aggregateRoot);
    }
}