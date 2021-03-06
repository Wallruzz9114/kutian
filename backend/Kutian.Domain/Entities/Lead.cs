using System;
using Kutian.Domain.Events;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class Lead : AggregateRoot
    {
        public Lead() => Apply(new LeadCreated());

        protected override void When(dynamic @event) => When(@event);

        protected void When(LeadCreated leadCreated) => LeadId = leadCreated.LeadId;

        protected void When(LeadRemoved leadRemoved) => Deleted = leadRemoved.Deleted;

        protected override void EnsureValidState() { }

        public void Remove() => Apply(new LeadRemoved());

        public Guid LeadId { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? Deleted { get; set; }
    }
}