using System;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Leads.Commands
{
    public class DeleteLead
    {
        public class Command : IRequest<Unit>
        {
            public Guid LeadId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var lead = await _databaseContext.FindAsync<Lead>(command.LeadId);
                lead.Remove();
                _databaseContext.Store(lead);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}