using CliTestApp;
using Skyware.Arenal.Model.Actions;

namespace ArenalIntegrationTests.Orders
{
    internal class UpdateOrderTests : BaseTestSetup
    {

        [Test]
        public async Task Update_Available_Order_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _publisher.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            orderGetResult.AddService(service).AddSample("SER", null, "S05FT9");

            Order orderUpdateResult = await _publisher.UpdateOrdersAsync(orderGetResult);
            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
            Assert.That(orderUpdateResult.Services.Count, Is.Not.EqualTo(order.Services.Count));
            Assert.That(orderUpdateResult.Samples.Count, Is.Not.EqualTo(order.Samples.Count));
            Assert.That(orderUpdateResult.Services.Any(p => p.Name == "Глюкоза"), Is.True);
            Assert.That(orderUpdateResult.Samples.Any(p => p.SampleType.TypeId.Value == "SER"), Is.True);
        }

        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task UpdateOrder_ByPublisher(string Status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _publisher.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));
            Order changedStateOrder = await _laboratory.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = Status });

            changedStateOrder.AddService(service).AddSample("SER", null, "S05FT9");
            Assert.ThrowsAsync<ArenalException>(async () => await _publisher.UpdateOrdersAsync(changedStateOrder));
        }

        [Test]
        public async Task UpdateOrder_Version_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _publisher.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            orderGetResult.Version = 3;
            Order orderUpdateResult = await _publisher.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateOrder_Created_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _publisher.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Created.HasValue, Is.True);
            Assert.That(orderGetResult.Created.Value.Date, Is.EqualTo(DateTime.UtcNow.Date));

            DateTime? origCreated = orderGetResult.Created;
            orderGetResult.Created = DateTime.Now.AddDays(-31);
            Order orderUpdateResult = await _publisher.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Created.HasValue, Is.True);
            Assert.That(orderUpdateResult.Created.Value, Is.EqualTo(origCreated));
        }

        [Test]
        public async Task UpdateOrder_ShortCode_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _publisher.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.ShortCode, Is.Not.Null);
            Assert.That(orderGetResult.ShortCode, Is.Not.Empty);
            string origShortCode = orderGetResult.ShortCode;

            orderGetResult.ShortCode = "SomeRandomShortCode7";
            Order orderUpdateResult = await _publisher.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.ShortCode, Is.Not.Null);
            Assert.That(orderUpdateResult.ShortCode, Is.Not.Empty);
            Assert.That(orderUpdateResult.ShortCode, Is.EqualTo(origShortCode));

        }

        [Test]
        public async Task UpdateOrder_TakenOrRejected_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _publisher.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.TakenOrRejected.HasValue, Is.False);

            orderGetResult.TakenOrRejected = DateTime.Now.AddDays(-31);
            Order orderUpdateResult = await _publisher.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.TakenOrRejected.HasValue, Is.False);
        }

        [Test]
        public async Task Update_Available_Order_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            orderGetResult.AddService(service).AddSample("SER", null, "S05FT9");
            Assert.ThrowsAsync<ArenalException>(async () => await _laboratory.UpdateOrdersAsync(orderGetResult));
        }

        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task Update_Order_ByLaboratory(string status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            Order changedStateOrder = await _laboratory.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = status });

            changedStateOrder.AddService(service).AddSample("SER", null, "S05FT9");
            Order orderUpdateResult = await _laboratory.UpdateOrdersAsync(changedStateOrder);
            Assert.IsNotNull(orderUpdateResult);


            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Status, Is.EqualTo(status));
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
            Assert.That(orderUpdateResult.Services.Count, Is.Not.EqualTo(order.Services.Count));
            Assert.That(orderUpdateResult.Samples.Count, Is.Not.EqualTo(order.Samples.Count));
            Assert.That(orderUpdateResult.Services.Any(p => p.Name == "Глюкоза"), Is.True);
            Assert.That(orderUpdateResult.Samples.Any(p => p.SampleType.TypeId.Value == "SER"), Is.True);
        }

        [Test]
        public async Task UpdateOrder_Version_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            Order changedStateOrder = await _laboratory.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = OrderStatuses.IN_PROGRESS });
            Assert.That(changedStateOrder.Version, Is.EqualTo(0));

            changedStateOrder.Version = 3;

            Order orderUpdateResult = await _laboratory.UpdateOrdersAsync(changedStateOrder);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateOrder_Created_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Created.HasValue, Is.True);
            Assert.That(orderGetResult.Created.Value.Date, Is.EqualTo(DateTime.UtcNow.Date));

            Order changedStateOrder = await _laboratory.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = OrderStatuses.IN_PROGRESS });
            Assert.That(changedStateOrder.Version, Is.EqualTo(0));
            
            DateTime? origCreated = changedStateOrder.Created;
            changedStateOrder.Created = DateTime.Now.AddDays(-31);
            Order orderUpdateResult = await _laboratory.UpdateOrdersAsync(changedStateOrder);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Created.HasValue, Is.True);
            Assert.That(orderUpdateResult.Created.Value, Is.EqualTo(origCreated));
        }

        [Test]
        public async Task UpdateOrder_ShortCode_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.ShortCode, Is.Not.Null);
            Assert.That(orderGetResult.ShortCode, Is.Not.Empty);
            string origShortCode = orderGetResult.ShortCode;

            Order changedStateOrder = await _laboratory.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = OrderStatuses.IN_PROGRESS });
            Assert.That(changedStateOrder.Version, Is.EqualTo(0));

            changedStateOrder.ShortCode = "SomeRandomShortCode7";
            Order orderUpdateResult = await _laboratory.UpdateOrdersAsync(changedStateOrder);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.ShortCode, Is.Not.Null);
            Assert.That(orderUpdateResult.ShortCode, Is.Not.Empty);
            Assert.That(orderUpdateResult.ShortCode, Is.EqualTo(origShortCode));

        }

        [Test]
        public async Task UpdateOrder_TakenOrRejected_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _laboratory.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.TakenOrRejected.HasValue, Is.False);

            Order changedStateOrder = await _laboratory.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = OrderStatuses.IN_PROGRESS });

            DateTime? origTakenOrRej = changedStateOrder.TakenOrRejected;
            changedStateOrder.TakenOrRejected = DateTime.Now.AddDays(-31);
            Order orderUpdateResult = await _laboratory.UpdateOrdersAsync(changedStateOrder);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.TakenOrRejected.HasValue, Is.True);
            Assert.That(orderUpdateResult.TakenOrRejected.Value, Is.EqualTo(origTakenOrRej));
        }
    }
}
