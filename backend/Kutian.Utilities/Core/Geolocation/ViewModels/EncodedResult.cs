using System.Collections.Generic;

namespace Kutian.Utilities.Core.Geolocation.ViewModels
{
    public class EncodedResult
    {
        public List<AddressComponent> AddressComponents { get; set; }
        public string Address { get; set; }
        public Geometry Geometry { get; set; }
        public string PlaceId { get; set; }
        public List<string> Types { get; set; }
    }
}