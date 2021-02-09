using System;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class Address : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState() { }

        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}