using Skyware.Arenal.Model;
using System.Collections.Generic;

namespace Skyware.Arenal.Discovery
{

    /// <summary>
    /// Pricelist item.
    /// </summary>
    public class CompendiumEntry : Service
    {

        /// <summary>
        /// Service offered.
        /// </summary>
        public Service Service { get; set; }

        /// <summary>
        /// Official provider's price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Required samples for given service (laboratory).
        /// </summary>
        public IEnumerable<SampleType> ReuiredSampleTypes { get; set; }

    }

}
