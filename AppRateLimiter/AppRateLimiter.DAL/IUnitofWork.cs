using AppRateLimiter.Models;

namespace AppRateLimiter.DAL
{
    public interface IUnitofWork
    {
        public IRepository<UserBucket> UserBucketRepository { get; }
        public void Save();
        public void Dispose(bool disposing);
    }
}
