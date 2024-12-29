using Microsoft.AspNetCore.Http;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface ICheckUrlService
    {
        public Task<string> CheckUrl(HttpRequest request);
    }
}
