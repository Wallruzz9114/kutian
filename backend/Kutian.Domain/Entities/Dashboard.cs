using System;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class Dashboard : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState() { }

        public Guid DashboardId { get; private set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public record DashboardCard
        {
            public Guid CardId { get; set; }
            public dynamic Options { get; set; }
        }
    }
}