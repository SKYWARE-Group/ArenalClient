using System.Collections.Generic;

namespace Skyware.Arenal.Discovery
{

    /// <summary>
    /// Service provider compendium (catalog, pricelist)
    /// </summary>
    public class Compendium
    {

        /// <summary>
        /// Arenal Id of the provider.
        /// </summary>
        public string ProviderId { get; set; }

        /// <summary>
        /// Primary currency for this provider.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// List of <see cref="CompendiumEntry"/> provided by this provider.
        /// </summary>
        public IEnumerable<CompendiumEntry> Services { get; set; }

    }

}
