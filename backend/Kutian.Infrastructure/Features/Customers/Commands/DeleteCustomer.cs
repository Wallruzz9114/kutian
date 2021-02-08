using System;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Customers.Commands
{
    public class DeleteCustomer
    {
        public class Command : IRequest<Unit>
        {
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var customer = await _databaseContext.FindAsync<Customer>(command.CustomerId);
                _databaseContext.Store(customer);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}