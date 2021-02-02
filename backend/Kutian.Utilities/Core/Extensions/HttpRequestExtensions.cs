using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Kutian.Utilities.Core.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetAccessToken(this HttpRequest httpRequest, string scheme = "Bearer")
        {
            httpRequest.Headers.TryGetValue("Authorization", out StringValues stringValue);

            if (StringValues.IsNullOrEmpty(stringValue))
                stringValue = httpRequest.Query["access_token"];

            return stringValue.ToString().Replace($"{ scheme } ", "");
        }
    }
}