using System;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Orders.Commands
{
    public class RemoveOrder
    {
        public class Command : IRequest<Unit>
        {
            public Guid OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var order = await _databaseContext.FindAsync<Order>(command.OrderId);
                _databaseContext.Store(order);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}