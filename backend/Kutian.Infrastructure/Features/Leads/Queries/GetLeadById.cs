using System;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Leads.Queries
{
    public class GetLeadById
    {
        public class Query : IRequest<LeadResponse>
        {
            public Guid LeadId { get; set; }
        }

        public class Handler : IRequestHandler<Query, LeadResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<LeadResponse> Handle(Query query, CancellationToken cancellationToken)
            {
                var lead = await _databaseContext.FindAsync<Lead>(query.LeadId);
                return new LeadResponse { Lead = lead.ToDTO() };
            }
        }
    }
}