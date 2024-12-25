namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IReadService
    {
        Task<string> ReadAsync(string url);
    }
}
