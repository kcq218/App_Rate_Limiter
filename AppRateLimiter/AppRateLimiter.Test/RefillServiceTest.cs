using AppRateLimiter.CheckBucketReadService.Services;
using AppRateLimiter.DAL;
using AppRateLimiter.Models;
using Moq;

namespace AppRateLimiter.Test
{
    [TestClass]
    public sealed class RefillServiceTest
    {
        private Mock<IUnitofWork>? _MockUnitofWork;
        private UserBucket? _MockUser;
        private IRefillService? _RefillService;

        [TestInitialize]
        public void Initialize()
        {
            _MockUnitofWork = new Mock<IUnitofWork>();
            _MockUnitofWork.Setup(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>())).Verifiable();
            _MockUnitofWork.Setup(m => m.Save()).Verifiable();
            _RefillService = new Refillservice(_MockUnitofWork.Object);
            _MockUser = TestData.MockUser();
        }

        [TestMethod]
        public void BucketShouldBeRefilled()
        {
            if (_MockUser != null && _RefillService != null)
            {
                _MockUser.LastAccessed = DateTime.Now.AddSeconds(-30);
                _RefillService.RefillBucket(_MockUser);
                _MockUnitofWork?.Verify(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>()), Times.Once);
                _MockUnitofWork?.Verify(m => m.Save(), Times.Once);
            }
        }

        [TestMethod]
        public void BucketShouldNotBeRefilled()
        {
            if (_MockUser != null && _RefillService != null)
            {
                _MockUser.LastAccessed = DateTime.Now.AddSeconds(-29);
                _RefillService.RefillBucket(_MockUser);
                _MockUnitofWork?.Verify(m => m.UserBucketRepository.Update(It.IsAny<UserBucket>()), Times.Never);
                _MockUnitofWork?.Verify(m => m.Save(), Times.Never);
            }
        }
    }
}
