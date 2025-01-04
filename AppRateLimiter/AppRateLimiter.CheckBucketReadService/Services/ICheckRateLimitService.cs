using AppRateLimiter.Models;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface ICheckRateLimitService
    {
        public Task<bool> WithinRateLimit(UserBucket user);
    }
}
