using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kutian.Utilities.Abstractions
{
    public interface IAggregateSet
    {
        IQueryable<TAggregateRoot> Set<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;
    }
}