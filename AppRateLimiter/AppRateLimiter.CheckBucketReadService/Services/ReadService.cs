using System.Text;
using System.Text.Json;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class ReadService : IReadService
    {
        public async Task<string> ReadAsync(string urlInput)
        {
            var _httpClient = new HttpClient();
            var requestUri = "https://urlread.azurewebsites.net/api/read?";
            var requestContent = new StringContent(JsonSerializer.Serialize(new
            {
                url = urlInput,
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUri, requestContent);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
