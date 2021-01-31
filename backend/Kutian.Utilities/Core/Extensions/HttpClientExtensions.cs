using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kutian.Utilities.Core.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TResult> PostAsync<TResult>(this HttpClient httpClient, string url, HttpContent httpContent, string oauthToHeader = null)
        {
            if (!string.IsNullOrEmpty(oauthToHeader))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToHeader.Split(' ')[1]);

            httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var responseMessage = await httpClient.PostAsync(url, httpContent);

            return JsonConvert.DeserializeObject<TResult>(await responseMessage.Content.ReadAsStringAsync());
        }

        public static async Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string url, dynamic content, string oauthToHeader = null)
        {
            if (!string.IsNullOrEmpty(oauthToHeader))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToHeader.Split(' ')[1]);

            var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(url, stringContent);
        }

        public static async Task<TOut> PostAsAsync<TIn, TOut>(this HttpClient httpClient, string url, TIn content, string oauthToHeader = null)
        {
            if (!string.IsNullOrEmpty(oauthToHeader))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToHeader.Split(' ')[1]);

            var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var responseMessage = await httpClient.PostAsync(url, stringContent);

            var responseText = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TOut>(responseText);
        }

        public static async Task<TOut> PutAsAsync<TIn, TOut>(this HttpClient httpClient, string url, TIn content, string oauthToHeader = null)
        {
            if (!string.IsNullOrEmpty(oauthToHeader))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToHeader.Split(' ')[1]);

            var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var responseMessage = await httpClient.PutAsync(url, stringContent);

            return JsonConvert.DeserializeObject<TOut>(await responseMessage.Content.ReadAsStringAsync());
        }

        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url, string oauthToHeader = null)
        {
            if (!string.IsNullOrEmpty(oauthToHeader))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToHeader.Split(' ')[1]);

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, string url, string oauthToHeader = null)
        {
            if (!string.IsNullOrEmpty(oauthToHeader))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToHeader.Split(' ')[1]);

            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(url);

            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }
}