using Microsoft.AspNetCore.Http;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IGetAppId
    {
        public string GetAppId(HttpRequest req);
    }
}
