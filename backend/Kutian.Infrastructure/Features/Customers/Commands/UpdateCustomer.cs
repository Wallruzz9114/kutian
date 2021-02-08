using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Customers.Commands
{
    public class UpdateCustomer
    {
        public class Command : IRequest<CustomerResponse>
        {
            public CustomerDTO Customer { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Customer).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, CustomerResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<CustomerResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                var customer = await _databaseContext.FindAsync<Customer>(command.Customer.CustomerId);
                _databaseContext.Store(customer);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return new CustomerResponse() { Customer = customer.ToDTO() };
            }
        }
    }
}