namespace Kutian.Domain.Events
{
    public class PhotoGalleryCreated
    {
        public PhotoGalleryCreated(string name) => Name = name;

        public string Name { get; }
    }
}