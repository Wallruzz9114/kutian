using System.Threading.Tasks;
using Kutian.Utilities.Core.Geolocation.ViewModels;

namespace Kutian.Utilities.Abstractions
{
    public interface IGoogleMapsService
    {
        Task<GoogleEncodedResponse> GetCoordinates(string address);
        Task<GoogleEncodedResponse> GetAddress(double latitude, double longitude);
    }
}