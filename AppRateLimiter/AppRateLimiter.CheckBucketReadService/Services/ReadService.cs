using AppRateLimiter.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class ReadService : IReadService
    {
        private HttpRequest? _req;

        public async Task<string> ReadAsync(HttpRequest request)
        {
            _req = request;
            string? url = _req.Query["url"].FirstOrDefault();
            string requestBody = await new StreamReader(_req.Body).ReadToEndAsync();
            dynamic? data = JsonConvert.DeserializeObject(requestBody);
            url = url ?? data?.url;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var query = new Dictionary<string, string?>
            {
                ["url"] = url
            };

            var urlShortener = Globals.ReadUrl;
            var response = await client.GetAsync(QueryHelpers.AddQueryString(urlShortener, query));
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<string>(responseBody) ?? string.Empty;
        }
    }
}
