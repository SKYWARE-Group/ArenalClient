using CliTestApp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenalIntegrationTests.Orders
{
    [TestFixture]
    internal class ChangeOrderStatusTests : SingleRoleBaseTestSetup
    {
        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        [TestCase("#x")] //invalid status
        public async Task ChangeStatus_From_Available_ByPublisher(string status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            Assert.ThrowsAsync<ArenalException>(async () => await _publisher.ChangeOrderStatusAsync(orderResult,
                new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = status }));
        }

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task ChangeStatus_From_Available_ByLaboratory(string status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.Not.True);
            Order statusChangedOrder = await _laboratory.ChangeOrderStatusAsync(orderResult,
                new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = status });

            Assert.IsNotNull(statusChangedOrder);
            Assert.That(statusChangedOrder.Version, Is.EqualTo(0));
            Assert.That(statusChangedOrder.TakenOrRejected.HasValue, Is.True);
            Assert.That(statusChangedOrder.Status, Is.EqualTo(status));
        }

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.COMPLETE)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.COMPLETE_WITH_PROBLEMS)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.IN_PROGRESS)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.REJECTED)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.REJECTED)]

        [TestCase(OrderStatuses.COMPLETE, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.IN_PROGRESS, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS, OrderStatuses.AVAILABLE)]
        [TestCase(OrderStatuses.REJECTED, OrderStatuses.AVAILABLE)]
        public async Task ChangeStatus_From_To_ByPublisher(string from, string to)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.Not.True);
            Order statusChangedOrder = await _laboratory.ChangeOrderStatusAsync(orderResult,
                new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = from });

            Assert.ThrowsAsync<ArenalException>(async () => await _publisher.ChangeOrderStatusAsync(statusChangedOrder,
                new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = to }));
        }

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.REJECTED)]
        public async Task ChangeStatus_From_To_ByLaboratory(string status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Assert.That(orderResult.TakenOrRejected.HasValue, Is.Not.True);
            Order statusChangedOrder = await _laboratory.ChangeOrderStatusAsync(orderResult,
                new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = status });

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
        public async Task ChangeStatus_To_Available_ByLaboratory(string status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);
            Order changedStatusOrder = await _laboratory.ChangeOrderStatusAsync(orderResult,
                            new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = status });

            Order changedStatusOrder2 = await _laboratory.ChangeOrderStatusAsync(changedStatusOrder,
                            new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = OrderStatuses.AVAILABLE });

            Assert.IsNotNull(changedStatusOrder2);
            Assert.IsNull(changedStatusOrder2.TakenOrRejected);
        }
    }
}
