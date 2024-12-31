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

        public async Task RefillBucketAsync(string appId)
        {
            // Refill bucket logic
            var bucketRepo = await _unitofWork.UserBucketRepository.FindAsync(m => m.ClientId == appId);
        }
    }
}
