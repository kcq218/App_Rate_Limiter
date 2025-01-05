using Microsoft.AspNetCore.Http;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IReadService
    {
        Task<string> ReadAsync(HttpRequest request);
    }
}
