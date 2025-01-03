using AppRateLimiter.Models;
using Microsoft.AspNetCore.Http;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public interface IGetAppUser
    {
        public Task<IEnumerable<UserBucket>> GetUser(HttpRequest req);
    }
}
