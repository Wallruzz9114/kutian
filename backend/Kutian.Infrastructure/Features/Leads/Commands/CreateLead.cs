using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;
using Kutian.Domain.Extensions;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Leads.Commands
{
    public class CreateLead
    {
        public class Command : IRequest<LeadResponse>
        {
            public LeadDTO Lead { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Lead).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, LeadResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<LeadResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                var lead = new Lead();

                _databaseContext.Store(lead);

                await _databaseContext.SaveChangesAsync(cancellationToken);

                return new LeadResponse() { Lead = lead.ToDTO() };
            }
        }
    }
}