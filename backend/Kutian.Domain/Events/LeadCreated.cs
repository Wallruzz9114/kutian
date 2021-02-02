using System;

namespace Kutian.Domain.Events
{
    public class LeadCreated
    {
        public LeadCreated() => LeadId = Guid.NewGuid();

        public Guid LeadId { get; }
    }
}