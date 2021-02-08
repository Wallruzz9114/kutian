using System;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class Address : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState() { }

        public Guid AddressId { get; set; }
    }
}