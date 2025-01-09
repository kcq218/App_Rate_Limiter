using AppRateLimiter.CheckBucketReadService.Services;
using AppRateLimiter.DAL;
using AppRateLimiter.Models;
using Moq;

namespace AppRateLimiter.Test
{
    [TestClass]
    public sealed class CheckRateLimitServiceTest
    {
        private Mock<IUnitofWork> _MockUnitofWork;
        private UserBucket _MockUser;
        private ICheckRateLimitService _CheckRateLimitService;

        [TestInitialize]
        public void Initialize()
        {
            //_TestData = new TestData();
            _MockUnitofWork = new Mock<IUnitofWork>();
            _MockUnitofWork.Setup(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>())).Verifiable();
            _MockUnitofWork.Setup(m => m.Save()).Verifiable();
            _CheckRateLimitService = new CheckRateLimitService(_MockUnitofWork.Object);
            _MockUser = TestData.MockUser();
        }

        [TestMethod]
        public async Task UserShouldBeWithinRateLimit()
        {
            _MockUser.BucketCount = 2;
            var result = await _CheckRateLimitService.WithinRateLimit(_MockUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UserShouldNotBeWithinRateLimitWhenZero()
        {
            _MockUser.BucketCount = 0;
            var result = await _CheckRateLimitService.WithinRateLimit(_MockUser);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateAndSaveIsCalledOnce()
        {
            _MockUser.BucketCount = 0;
            var result = await _CheckRateLimitService.WithinRateLimit(_MockUser);
            _MockUnitofWork.Verify(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>()), Times.Once);
            _MockUnitofWork.Verify(m => m.Save(), Times.Once);
        }
    }
}
