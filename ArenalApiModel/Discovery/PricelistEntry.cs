using Skyware.Arenal.Model;

namespace Skyware.Arenal.Discovery
{

    /// <summary>
    /// Pricelist item.
    /// </summary>
    public class PricelistEntry : Service
    {

        /// <summary>
        /// Official provider's price.
        /// </summary>
        public decimal Price { get; set; }

    }

}
