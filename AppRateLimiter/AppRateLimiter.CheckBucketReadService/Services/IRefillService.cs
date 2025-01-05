using AppRateLimiter.Models;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IRefillService
    {
        public void RefillBucket(UserBucket user);
    }
}
