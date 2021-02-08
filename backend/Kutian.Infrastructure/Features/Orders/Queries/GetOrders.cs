using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kutian.Infrastructure.Features.Orders.Queries
{
    public class GetOrders
    {
        public class Query : IRequest<GetOrdersResponse> { }

        public class Handler : IRequestHandler<Query, GetOrdersResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<GetOrdersResponse> Handle(Query query, CancellationToken cancellationToken) =>
                new GetOrdersResponse()
                {
                    Orders = await _databaseContext.Set<Order>().Select(x => x.ToDTO()).ToListAsync(cancellationToken),
                };
        }
    }
}