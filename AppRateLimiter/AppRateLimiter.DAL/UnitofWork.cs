using AppRateLimiter.Models;

namespace AppRateLimiter.DAL
{
    public class UnitofWork : IUnitofWork
    {
        private DbAll01ProdUswest001Context _context = new DbAll01ProdUswest001Context();

        public IRepository<UserBucket> UserBucketRepository => new Repository<UserBucket>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}