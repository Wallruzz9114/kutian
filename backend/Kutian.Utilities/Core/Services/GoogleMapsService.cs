using System.Net.Http;
using System.Threading.Tasks;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Extensions;
using Kutian.Utilities.Core.Geolocation.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Kutian.Utilities.Core.Services
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<GoogleEncodedResponse> GetAddress(double latitude, double longitude)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&sensor=false&key={_configuration["GoogleMapsPlatform:ApiKey"]}";
            return await _httpClient.GetAsync<GoogleEncodedResponse>(url);
        }

        public async Task<GoogleEncodedResponse> GetCoordinates(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&sensor=false&key={_configuration["GoogleMapsPlatform:ApiKey"]}";
            return await _httpClient.GetAsync<GoogleEncodedResponse>(url);
        }
    }
}