using System.Collections.Generic;

namespace Kutian.Utilities.Core.Geolocation.ViewModels
{
    public class AddressComponent
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public List<string> Types { get; set; }
    }
}