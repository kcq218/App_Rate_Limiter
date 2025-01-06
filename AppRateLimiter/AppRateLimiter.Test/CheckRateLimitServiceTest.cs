using AppRateLimiter.DAL;
using Moq;

namespace AppRateLimiter.Test
{
    [TestClass]
    public sealed class CheckRateLimitServiceTest
    {
        private Mock<IUnitofWork> _MockUnitofWork;
        //private TestData _TestData;

        [TestInitialize]
        public void Initialize()
        {
            //_TestData = new TestData();
            _MockUnitofWork = new Mock<IUnitofWork>();

            _MockUnitofWork.Setup(m => m.Save()).Verifiable();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
