using AppRateLimiter.Models;

namespace AppRateLimiter.Test
{
    public static class TestData
    {
        public static UserBucket MockUser()
        {
            return new UserBucket
            {
                BucketCount = 2,
                ClientId = "test",
                CreatedBy = "test",
                CreatedDate = System.DateTime.Now,
                Id = 1,
                LastAccessed = System.DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = System.DateTime.Now,
                BucketLimit = 2,
                RefillRateSeconds = 30
            };
        }
    }
}
