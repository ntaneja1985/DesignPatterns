using DesignPatterns;
using Moq;

namespace xUnitTestProject
{
    public class UnitTest1
    {
        public bool FakeSave()
        {
            return true;
        }
        [Fact]
        public void Test1()
        {
            var mockRepo = new Mock<IRepository<Customer>>();
            mockRepo.Setup(x=>x.Save(new Customer())).Returns(FakeSave);

            IRepository<Customer> rep = FactoryRepository<Customer>.Create();
            var result = rep.Save(new Customer());
            Assert.True(result);
        }
    }
}