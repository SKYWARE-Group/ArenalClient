using CliTestApp;

namespace ArenalIntegrationTests.Orders
{
    [TestFixture]
    internal class CreateOrderTests : BaseTestSetup
    {
        [Test]
        public async Task CreateOrder_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
        }

        [Test]
        public void CreateOrder_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _laboratory.CreateOrdersAsync(order)));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.Unauthorized);
        }
    }
}
