using System.Collections.Generic;
using System.Linq;

namespace Kutian.Utilities.Core.Geolocation.ViewModels
{
    public class GoogleEncodedResponse
    {
        public List<EncodedResult> Results { get; set; }
        public string Status { get; set; }
        public double Longitude { get => Results.ElementAt(0).Geometry.Location.Longitude; }
        public double Latitude { get => Results.ElementAt(0).Geometry.Location.Longitude; }
    }
}