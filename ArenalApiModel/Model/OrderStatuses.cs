using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Contains constant values for Order status
    /// </summary>
    public class OrderStatuses
    {

        /// <summary>
        /// Available
        /// </summary>
        public const string FREE = "free";

        /// <summary>
        /// Taken (Locked)
        /// </summary>
        public const string TAKEN = "taken";

        public const string ACCEPTED = "accepted";

        public const string PARTIALLY_ACCEPTED = "partially-accepted";

    }
}
