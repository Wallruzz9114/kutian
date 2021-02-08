using System;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class DigitalAsset : AggregateRoot
    {
        public DigitalAsset(string name, byte[] bytes, string contentType) { }

        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState() { }

        public Guid DigitalAssetId { get; private set; }
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
    }
}