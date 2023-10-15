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

            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _publisher.DeleteOrdersAsync(orderResult)));
            Assert.IsNotNull(ex);
            // It is locked
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteOrder_ByLaboratory()
        {
            Order order = FakeOrders.Generate(_publisherId, _laboratoryId);
            Order orderResult = await _publisher.CreateOrdersAsync(order);

            Assert.IsNotNull(orderResult);

            var ex = Assert.ThrowsAsync<ArenalException>(new AsyncTestDelegate(async () => await _laboratory.DeleteOrdersAsync(orderResult)));
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.HttpStatusCode == (int)HttpStatusCode.NotFound);
        }
    }
}
