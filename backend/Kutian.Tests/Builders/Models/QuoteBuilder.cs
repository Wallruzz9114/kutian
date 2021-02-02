using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class QuoteBuilder
    {
        private readonly Quote _quote;

        public QuoteBuilder() => _quote = new Quote();

        public Quote Build() => _quote;
    }
}