using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;

namespace Kutian.Infrastructure.Features.Quotes.Queries
{
    public class CalculateQuote
    {
        public class Query : IRequest<QuoteResponse>
        {
            public int Hours { get; set; }
        }

        public class Handler : IRequestHandler<Query, QuoteResponse>
        {
            private readonly IDatabaseContext _databaseContext;

            public Handler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

            public async Task<QuoteResponse> Handle(Query query, CancellationToken cancellationToken) =>
                new QuoteResponse { };
        }
    }
}