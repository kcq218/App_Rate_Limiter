using AppRateLimiter.Models;
using Microsoft.AspNetCore.Http;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface ICheckAppId
    {
        public Task<IEnumerable<UserBucket>> GetAppId(HttpRequest req);
    }
}
