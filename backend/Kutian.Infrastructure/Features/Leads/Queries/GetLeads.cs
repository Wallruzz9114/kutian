using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kutian.Infrastructure.Features.Leads.Queries
{
    public class GetLeads
    {
        public class Query : IRequest<GetLeadsResponse> { }

        public class Handler : IRequestHandler<Query, GetLeadsResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<GetLeadsResponse> Handle(Query query, CancellationToken cancellationToken) =>
                new GetLeadsResponse
                {
                    Leads = await _databaseContext
                        .Set<Lead>()
                        .Select(x => x.ToDTO())
                        .ToListAsync(cancellationToken: cancellationToken)
                };
        }
    }
}