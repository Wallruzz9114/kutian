using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Extensions;
using MediatR;

namespace Kutian.Infrastructure.Features.Orders.Commands
{
    public class UpdateOrder
    {
        public class Command : IRequest<OrderResponse>
        {
            public OrderDTO Order { get; set; }
        }

        public class Handler : IRequestHandler<Command, OrderResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<OrderResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                var order = await _databaseContext.FindAsync<Order>(command.Order.OrderId);
                _databaseContext.Store(order);

                await _databaseContext.SaveChangesAsync(cancellationToken);

                return new OrderResponse() { Order = order.ToDTO() };
            }
        }
    }
}