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

        public void RefillBucket(UserBucket user)
        {
            // Refill bucket logic
            // set last accessed if refill
            var timeLastAccessed = user.LastAccessed;
            var timeNow = DateTime.Now;
            var timeDifference = timeNow - timeLastAccessed;

            if (timeDifference.HasValue && user.RefillRateSeconds.HasValue
                && timeDifference.Value.TotalSeconds >= user.RefillRateSeconds.Value)
            {
                user.BucketCount = user.BucketLimit;
                user.LastAccessed = DateTime.Now;
                user.UpdatedDate = DateTime.Now;
                user.UpdatedBy = user.ClientId ?? string.Empty;
                _unitofWork.UserBucketRepository.Update(user);
                _unitofWork.Save();
            }
        }
    }
}
