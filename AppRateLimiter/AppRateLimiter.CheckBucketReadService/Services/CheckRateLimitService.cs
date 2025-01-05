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
            // we want to set last accessed only when user hit 0

            var result = false;
            user.BucketCount = --user.BucketCount;
            if (user.BucketCount == 0)
            {
                user.LastAccessed = DateTime.Now;
            }
            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = user.ClientId ?? string.Empty;
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
