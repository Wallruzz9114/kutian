using System;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class Contact : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState() { }

        public Guid ContactId { get; private set; }
    }
}