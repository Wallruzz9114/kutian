using System.Threading;
using System.Threading.Tasks;
using Kutian.Utilities.Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Kutian.Utilities.Abstractions
{
    public interface IEventStoreDatabaseContext
    {
        DbSet<StoredEvent> StoredEvents { get; }
        DbSet<Snapshot> SnapShots { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}