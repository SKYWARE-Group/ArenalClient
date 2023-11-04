using CliTestApp;
using Skyware.Arenal.Model.Actions;

namespace ArenalIntegrationTests.Orders
{
    internal class DoubleRoleScenariosTests : DoubleRoleBaseTestSetup
    {
        private Order GenerateFakeOrder()
        {
            Order order = FakeOrders.Generate(_actorId, null);
            order.Workflow = Workflows.LAB_MCP;
            return order;
        }

        #region CreateOrders

        [Test]
        public async Task CreateOrder()
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
        }

        [Test]
        public async Task CreateOrder_Diff_Version()
        {
            Order order = GenerateFakeOrder();
            order.Version = 7;
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderResult.Version == 0);

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderGetResult.Version == 0);
        }

        [Test]
        public async Task CreateOrder_Diff_Status()
        {
            Order order = GenerateFakeOrder();
            order.Status = OrderStatuses.IN_PROGRESS;
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderResult.Version == 0);

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.IsTrue(orderGetResult.Version == 0);
        }

        [Test]
        public async Task CreateOrder_Diff_Created()
        {
            Order order = GenerateFakeOrder();
            order.Created = DateTime.Now.AddDays(-31);
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderResult.Created.HasValue, Is.True);
            Assert.That(orderResult.Created.Value, Is.Not.EqualTo(order.Created));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderGetResult.Created.HasValue, Is.True);
            Assert.That(orderGetResult.Created.Value, Is.Not.EqualTo(order.Created));
        }

        [Test]
        public async Task CreateOrder_Diff_TakenOrRejected()
        {
            Order order = GenerateFakeOrder();
            order.TakenOrRejected = DateTime.Now.AddDays(-31);
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.False);

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderGetResult.TakenOrRejected.HasValue, Is.False);
        }

        [Test]
        public async Task CreateOrder_ShortCode()
        {
            Order order = GenerateFakeOrder();
            order.ShortCode = "SomeShortCode7";
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderResult.ShortCode, Is.Not.EqualTo("SomeShortCode7"));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);

            Assert.IsNotNull(orderGetResult);
            Assert.IsTrue(orderGetResult.Status == OrderStatuses.AVAILABLE);
            Assert.That(orderGetResult.ShortCode, Is.Not.EqualTo("SomeShortCode7"));
        }

        #endregion

        #region UpdateOrders

        [Test]
        public async Task Update_Available_Order()
        {
            Order order = GenerateFakeOrder();
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            orderGetResult.AddService(service).AddSample("SER", null, "S05FT9");

            Order orderUpdateResult = await _actor.UpdateOrdersAsync(orderGetResult);
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
        public async Task UpdateOrder(string Status)
        {
            Order order = GenerateFakeOrder();
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));
            Order changedStateOrder = await _actor.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = Status });

            changedStateOrder.AddService(service).AddSample("SER", null, "S05FT9");

            Order orderUpdateResult = await _actor.UpdateOrdersAsync(changedStateOrder);
            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Status, Is.EqualTo(changedStateOrder.Status));
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
            Assert.That(orderUpdateResult.Services.Count, Is.Not.EqualTo(order.Services.Count));
            Assert.That(orderUpdateResult.Samples.Count, Is.Not.EqualTo(order.Samples.Count));
            Assert.That(orderUpdateResult.Services.Any(p => p.Name == "Глюкоза"), Is.True);
            Assert.That(orderUpdateResult.Samples.Any(p => p.SampleType.TypeId.Value == "SER"), Is.True);
        }

        [Test]
        public async Task UpdateOrder_Version()
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            orderGetResult.Version = 3;
            Order orderUpdateResult = await _actor.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateOrder_Created()
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Created.HasValue, Is.True);
            Assert.That(orderGetResult.Created.Value.Date, Is.EqualTo(DateTime.UtcNow.Date));

            DateTime? origCreated = orderGetResult.Created;
            orderGetResult.Created = DateTime.Now.AddDays(-31);
            Order orderUpdateResult = await _actor.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Created.HasValue, Is.True);
            Assert.That(orderUpdateResult.Created.Value, Is.EqualTo(origCreated));
        }

        [Test]
        public async Task UpdateOrder_ShortCode()
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.ShortCode, Is.Not.Null);
            Assert.That(orderGetResult.ShortCode, Is.Not.Empty);
            string origShortCode = orderGetResult.ShortCode;

            orderGetResult.ShortCode = "SomeRandomShortCode7";
            Order orderUpdateResult = await _actor.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.ShortCode, Is.Not.Null);
            Assert.That(orderUpdateResult.ShortCode, Is.Not.Empty);
            Assert.That(orderUpdateResult.ShortCode, Is.EqualTo(origShortCode));

        }

        [Test]
        public async Task UpdateOrder_TakenOrRejected()
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.TakenOrRejected.HasValue, Is.False);

            orderGetResult.TakenOrRejected = DateTime.Now.AddDays(-31);
            Order orderUpdateResult = await _actor.UpdateOrdersAsync(orderGetResult);

            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.TakenOrRejected.HasValue, Is.False);
        }

        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task Update_Order(string status)
        {
            Order order = GenerateFakeOrder();
            Service service = new Service("14749-6", "Глюкоза")
                .AddAlternateIdentifier(Authorities.BG_HIS, Dictionaries.BG_NHIS_CL022, "03-002")
                .AddAlternateIdentifier(Authorities.BG_HIF, Dictionaries.BG_NHIF_Product, "01.11")
                .AddAlternateIdentifier(Authorities.LOCAL, null, "0-155");

            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.Status, Is.EqualTo(OrderStatuses.AVAILABLE));

            Order orderGetResult = await _actor.GetOrderAsync(orderResult.ArenalId);
            Assert.That(orderGetResult.Version, Is.EqualTo(0));

            Order changedStateOrder = await _actor.ChangeOrderStatusAsync(orderGetResult, new OrderStatusRequest() { NewStatus = status });

            changedStateOrder.AddService(service).AddSample("SER", null, "S05FT9");
            Order orderUpdateResult = await _actor.UpdateOrdersAsync(changedStateOrder);
            Assert.IsNotNull(orderUpdateResult);


            Assert.IsNotNull(orderUpdateResult);
            Assert.That(orderUpdateResult.Status, Is.EqualTo(status));
            Assert.That(orderUpdateResult.Version, Is.EqualTo(1));
            Assert.That(orderUpdateResult.Services.Count, Is.Not.EqualTo(order.Services.Count));
            Assert.That(orderUpdateResult.Samples.Count, Is.Not.EqualTo(order.Samples.Count));
            Assert.That(orderUpdateResult.Services.Any(p => p.Name == "Глюкоза"), Is.True);
            Assert.That(orderUpdateResult.Samples.Any(p => p.SampleType.TypeId.Value == "SER"), Is.True);
        }

        #endregion

        #region Delete

        [Test]
        public async Task DeleteOrder()
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            await _actor.DeleteOrdersAsync(orderResult);

            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _actor.GetOrderAsync(orderResult.ArenalId)));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.NotFound);
        }

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        public async Task Delete_Unavailable_Order(string status)
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            Order statusChangedOrderResult = await _actor.ChangeOrderStatusAsync(orderResult, new OrderStatusRequest() { NewStatus = status });
            Assert.IsNotNull(statusChangedOrderResult);

            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _actor.DeleteOrdersAsync(statusChangedOrderResult)));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.BadRequest);
        }

        #endregion

        #region Change Status

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task ChangeStatus_From_Available(string status)
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.Not.True);
            Order statusChangedOrder = await _actor.ChangeOrderStatusAsync(orderResult,
                new OrderStatusRequest() { NewStatus = status });

            Assert.IsNotNull(statusChangedOrder);
            Assert.That(statusChangedOrder.Version, Is.EqualTo(0));
            Assert.That(statusChangedOrder.TakenOrRejected.HasValue, Is.True);
            Assert.That(statusChangedOrder.Status, Is.EqualTo(status));
        }

        //[TestCase(OrderStatuses.COMPLETE, OrderStatuses.COMPLETE)] Unapplicable
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.COMPLETE)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        //[TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.COMPLETE_WITH_PROBLEMS)] Unapplicable
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.COMPLETE_WITH_PROBLEMS)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS)]
        //[TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.IN_PROGRESS)]Unapplicable
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.IN_PROGRESS)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        //[TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]Unapplicable
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.REJECTED)]
        //[TestCase(OrderStatuses.REJECTED, OrderStatuses.REJECTED)] Unapplicable

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.AVAILABLE)]
        public async Task ChangeStatus_From_To(string from, string to)
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.Not.True);
            Order statusChangedOrder = await _actor.ChangeOrderStatusAsync(orderResult, new OrderStatusRequest() { NewStatus = from });

            Order sut = await _actor.ChangeOrderStatusAsync(statusChangedOrder, new OrderStatusRequest() { NewStatus = to });
            Assert.IsNotNull(sut);
            Assert.That(sut.ArenalId, Is.EqualTo(orderResult.ArenalId));
            Assert.That(sut.Status, Is.EqualTo(to));
        }

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task ChangeStatus_From_To(string status)
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.Not.True);
            Order statusChangedOrder = await _actor.ChangeOrderStatusAsync(orderResult,
                new OrderStatusRequest() { NewStatus = status });

            Assert.IsNotNull(statusChangedOrder);
            Assert.That(statusChangedOrder.Version, Is.EqualTo(0));
            Assert.That(statusChangedOrder.TakenOrRejected.HasValue, Is.True);
            Assert.That(statusChangedOrder.Status, Is.EqualTo(status));
        }

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task ChangeStatus_To_Available(string status)
        {
            Order order = GenerateFakeOrder();
            Order orderResult = await _actor.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Order changedStatusOrder = await _actor.ChangeOrderStatusAsync(orderResult,
                            new OrderStatusRequest() { NewStatus = status });

            Order changedStatusOrder2 = await _actor.ChangeOrderStatusAsync(changedStatusOrder,
                            new OrderStatusRequest() { NewStatus = OrderStatuses.AVAILABLE });

            Assert.IsNotNull(changedStatusOrder2);
            Assert.IsNull(changedStatusOrder2.TakenOrRejected);
        }

        #endregion
    }
}
