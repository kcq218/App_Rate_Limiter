using AppRateLimiter.DAL;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class Refillservice : IRefillService
    {
        IUnitofWork _unitofWork;

        public Refillservice(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void RefillBucket()
        {
            // Refill bucket logic
            var bucketRepo = _unitofWork.UserBucketRepository;


        }
    }
}
