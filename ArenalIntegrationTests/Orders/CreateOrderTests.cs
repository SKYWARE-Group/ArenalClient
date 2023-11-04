using CliTestApp;

namespace ArenalIntegrationTests.Orders
{
    [TestFixture]
    internal class CreateOrderTests : SingleRoleBaseTestSetup
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
        public async Task CreateOrder_Diff_Version_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            order.Version = 7;
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderResult.Version == 0);

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderGetResult.Version == 0);
        }

        [Test]
        public async Task CreateOrder_Diff_Status_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            order.Status = OrderStatuses.IN_PROGRESS;
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderResult.Version == 0);

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderGetResult.Version == 0);
        }

        [Test]
        public async Task CreateOrder_Diff_Created_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            order.Created = DateTime.Now.AddDays(-31);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderResult.Created.HasValue, Is.True);
            Assert.That(orderResult.Created.Value, Is.Not.EqualTo(order.Created));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderGetResult.Created.HasValue, Is.True);
            Assert.That(orderGetResult.Created.Value, Is.Not.EqualTo(order.Created));
        }
        
        [Test]
        public async Task CreateOrder_Diff_TakenOrRejected_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            order.TakenOrRejected = DateTime.Now.AddDays(-31);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.False);

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderGetResult.TakenOrRejected.HasValue, Is.False);
        }

        [Test]
        public async Task CreateOrder_ShortCode_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            order.ShortCode = "SomeShortCode7";
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderResult.ShortCode, Is.Not.EqualTo("SomeShortCode7"));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderGetResult.ShortCode, Is.Not.EqualTo("SomeShortCode7"));
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
