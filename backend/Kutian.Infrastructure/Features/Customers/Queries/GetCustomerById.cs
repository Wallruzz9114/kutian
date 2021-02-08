using System;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Customers.Queries
{
    public class GetCustomerById
    {
        public class Query : IRequest<CustomerResponse>
        {
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, CustomerResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<CustomerResponse> Handle(Query query, CancellationToken cancellationToken)
            {
                var customer = await _databaseContext.FindAsync<Customer>(query.CustomerId);
                return new CustomerResponse() { Customer = customer.ToDTO() };
            }
        }
    }
}