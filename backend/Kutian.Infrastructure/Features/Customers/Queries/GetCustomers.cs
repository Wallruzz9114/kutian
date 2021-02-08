using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kutian.Infrastructure.Features.Customers.Queries
{
    public class GetCustomers
    {
        public class Query : IRequest<GetCustomersResponse> { }

        public class Handler : IRequestHandler<Query, GetCustomersResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<GetCustomersResponse> Handle(Query query, CancellationToken cancellationToken) =>
                new GetCustomersResponse()
                {
                    Customers = await _databaseContext
                        .Set<Customer>()
                        .Select(x => x.ToDTO())
                        .ToListAsync(cancellationToken)
                };
        }
    }
}