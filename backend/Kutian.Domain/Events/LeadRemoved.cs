using System;

namespace Kutian.Domain.Events
{
    public class LeadRemoved
    {
        public DateTime Deleted { get; } = DateTime.UtcNow;
    }
}