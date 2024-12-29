using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class CheckUrlService : ICheckUrlService
    {
        private HttpRequest? _req;
        public CheckUrlService()
        {
        }

        public async Task<string> CheckUrl(HttpRequest request)
        {
            _req = request;
            string? url = _req.Query["url"].FirstOrDefault();
            string requestBody = await new StreamReader(_req.Body).ReadToEndAsync();
            dynamic? data = JsonConvert.DeserializeObject(requestBody);
            url = url ?? data?.url;

            return url ?? string.Empty;
        }
    }
}
