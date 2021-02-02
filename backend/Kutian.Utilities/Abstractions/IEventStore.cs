using System.Threading;
using System.Threading.Tasks;

namespace Kutian.Utilities.Abstractions
{
    public interface IEventStore
    {
        void Store(AggregateRoot aggregateRoot);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}