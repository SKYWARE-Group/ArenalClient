using CliTestApp;
using Skyware.Arenal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenalIntegrationTests.Orders
{
    internal class DeleteOrderTests : BaseTestSetup
    {
        [Test]
        public async Task DeleteOrder_ByPublisher()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            await _publisher.DeleteOrdersAsync(orderResult);

            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _laboratory.GetOrderAsync(orderResult.ArenalId)));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.NotFound);
        }

        [TestCase(OrderStatuses.COMPLETE)]
        [TestCase(OrderStatuses.COMPLETE_WITH_PROBLEMS)]
        [TestCase(OrderStatuses.IN_PROGRESS)]
        [TestCase(OrderStatuses.IN_PROGRESS_WITH_PROBLEMS)]
        public async Task Delete_Unavailable_Order_ByPublisher(string status)
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            Order statusChangedOrderResult = await _laboratory.ChangeOrderStatusAsync(orderResult, new Skyware.Arenal.Model.Actions.OrderStatusRequest() { NewStatus = status });
            Assert.IsNotNull(statusChangedOrderResult);

            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _publisher.DeleteOrdersAsync(statusChangedOrderResult)));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteOrder_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            var ex = Assert.ThrowsAsync<ArenalException>(async () => await _laboratory.DeleteOrdersAsync(orderResult));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.NotFound);
        }
    }
}
