using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model.Events
{

    /// <summary>
    /// Occurs when <see cref="Order"/> is created, updated or deleted.
    /// </summary>
    public class OrderEvent
    {

        /// <summary>
        /// ArenalId of the order being processed.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// ArenalId of the service provider who interacted with the order.
        /// </summary>
        public string ProviderId { get; set; }

    }

}
