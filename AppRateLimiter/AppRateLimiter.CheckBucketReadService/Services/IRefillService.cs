namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IRefillService
    {
        public Task RefillBucketAsync(string appId);
    }
}
