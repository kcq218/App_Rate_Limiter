using AppRateLimiter.DAL;
using AppRateLimiter.Models;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class CheckRateLimitService : ICheckRateLimitService
    {
        private IUnitofWork _unitOfWork;

        public CheckRateLimitService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> WithinRateLimit(UserBucket user)
        {
            var result = false;
            user.BucketCount = --user.BucketCount;
            user.LastAccessed = DateTime.UtcNow;
            _unitOfWork.UserBucketRepository.Update(user);
            _unitOfWork.Save();

            if (user.BucketCount >= 0)
            {
                result = true;
            }

            return Task.FromResult(result);
        }
    }
}
