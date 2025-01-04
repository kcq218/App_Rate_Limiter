using AppRateLimiter.DAL;
using AppRateLimiter.Models;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class Refillservice : IRefillService
    {
        IUnitofWork _unitofWork;

        public Refillservice(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void RefillBucketAsync(UserBucket user)
        {
            // Refill bucket logic
            var timeLastAccessed = user.LastAccessed;
            var timeNow = DateTime.Now;
            var timeDifference = timeNow - timeLastAccessed;

            if (timeDifference.HasValue && user.RefillRateSeconds.HasValue
                && timeDifference.Value.Seconds >= user.RefillRateSeconds.Value)
            {
                user.BucketCount = user.BucketLimit;
                _unitofWork.UserBucketRepository.Update(user);
                _unitofWork.Save();
            }
        }
    }
}
