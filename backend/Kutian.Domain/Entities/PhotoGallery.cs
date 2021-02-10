using System;
using System.Collections.Generic;
using Kutian.Domain.Events;
using Kutian.Utilities.Abstractions;

namespace Kutian.Domain.Entities
{
    public class PhotoGallery : AggregateRoot
    {
        public PhotoGallery(string name) => Apply(new PhotoGalleryCreated(name));

        protected override void When(dynamic @event) => When(@event);

        protected void When(PhotoGalleryCreated photoGalleryCreated)
        {
            Name = photoGalleryCreated.Name;
            Photos = new HashSet<Photo>();
        }

        protected override void EnsureValidState() { }

        public Guid PhotoGalleryId { get; private set; }
        public string Name { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public record Photo
        {
            public Guid DigitalAssetId { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }
        }
    }
}