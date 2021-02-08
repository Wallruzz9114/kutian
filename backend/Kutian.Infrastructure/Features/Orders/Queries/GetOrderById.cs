using System;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Extensions;
using MediatR;

namespace Kutian.Infrastructure.Features.Orders.Queries
{
    public class GetOrderById
    {
        public class Query : IRequest<OrderResponse>
        {
            public Guid OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Query, OrderResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<OrderResponse> Handle(Query query, CancellationToken cancellationToken)
            {
                var order = await _databaseContext.FindAsync<Order>(query.OrderId);
                return new OrderResponse() { Order = order.ToDTO() };
            }
        }
    }
}