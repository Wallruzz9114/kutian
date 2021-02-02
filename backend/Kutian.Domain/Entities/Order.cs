using System;
using System.Collections.Generic;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class Order : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState() { }

        public Guid OrderId { get; private set; }
        public decimal Total { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

        public record LineItem { }
    }
}