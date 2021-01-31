using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kutian.Utilities.Abstractions
{
    public interface IDatabaseContext
    {
        IQueryable<T> Set<T>() where T : AggregateRoot;
        void Store(AggregateRoot aggregateRoot);
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}