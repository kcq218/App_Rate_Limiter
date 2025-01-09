using AppRateLimiter.CheckBucketReadService.Services;
using AppRateLimiter.DAL;
using AppRateLimiter.Models;
using Moq;

namespace AppRateLimiter.Test
{
    [TestClass]
    public sealed class CheckRateLimitServiceTest
    {
        private Mock<IUnitofWork>? _MockUnitofWork;
        private UserBucket? _MockUser;
        private CheckRateLimitService? _CheckRateLimitService;

        [TestInitialize]
        public void Initialize()
        {
            _MockUnitofWork = new Mock<IUnitofWork>();
            _MockUnitofWork.Setup(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>())).Verifiable();
            _MockUnitofWork.Setup(m => m.Save()).Verifiable();
            _CheckRateLimitService = new CheckRateLimitService(_MockUnitofWork.Object);
            _MockUser = TestData.MockUser();
        }

        [TestMethod]
        public async Task UserShouldBeWithinRateLimit()
        {
            if (_MockUser == null) throw new InvalidOperationException("Mock user is not initialized.");
            if (_CheckRateLimitService == null) throw new InvalidOperationException("CheckRateLimitService is not initialized.");
            _MockUser.BucketCount = 2;
            var result = await _CheckRateLimitService.WithinRateLimit(_MockUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UserShouldNotBeWithinRateLimitWhenZero()
        {
            if (_MockUser == null) throw new InvalidOperationException("Mock user is not initialized.");
            if (_CheckRateLimitService == null) throw new InvalidOperationException("CheckRateLimitService is not initialized.");
            _MockUser.BucketCount = 0;
            var result = await _CheckRateLimitService.WithinRateLimit(_MockUser);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateAndSaveIsCalledOnce()
        {
            if (_MockUser == null) throw new InvalidOperationException("Mock user is not initialized.");
            if (_CheckRateLimitService == null) throw new InvalidOperationException("CheckRateLimitService is not initialized.");
            if (_MockUnitofWork == null) throw new InvalidOperationException("Mock unit of work is not initialized."); // Added null check
            _MockUser.BucketCount = 0;
            var result = await _CheckRateLimitService.WithinRateLimit(_MockUser);
            _MockUnitofWork.Verify(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>()), Times.Once);
            _MockUnitofWork.Verify(m => m.Save(), Times.Once);
        }
    }
}
