using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class LeadBuilder
    {
        private readonly Lead _lead;

        public LeadBuilder() => _lead = new Lead();

        public Lead Build() => _lead;
    }
}