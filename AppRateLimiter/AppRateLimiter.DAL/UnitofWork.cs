using AppRateLimiter.Models;

namespace AppRateLimiter.DAL
{
    public class UnitofWork:IUnitofWork
    {
        private DbAll01ProdUswest001Context _context = new DbAll01ProdUswest001Context();

        public IRepository<UserBucket> GeneratedKeyRepository => new Repository<UserBucket>(_context);

    }
}
