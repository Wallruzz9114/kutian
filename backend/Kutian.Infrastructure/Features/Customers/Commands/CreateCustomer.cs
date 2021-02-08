using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Contexts;
using MediatR;

namespace Kutian.Infrastructure.Features.Customers.Commands
{
    public class CreateCustomer
    {
        public class Command : IRequest<CustomerResponse>
        {
            public CustomerDTO Customer { get; set; }
        }

        public class CreateCustomerValidator : AbstractValidator<Command>
        {
            public CreateCustomerValidator()
            {
                RuleFor(x => x.Customer).NotNull();
                RuleFor(x => x.Customer).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, CustomerResponse>
        {
            private readonly DatabaseContext _databaseContext;

            public Handler(DatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<CustomerResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                var customer = new Customer();
                _databaseContext.Store(customer);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return new CustomerResponse() { Customer = customer.ToDTO() };
            }
        }
    }
}