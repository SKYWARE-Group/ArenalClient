using System.Collections.Generic;

namespace Skyware.Arenal.Discovery
{

    /// <summary>
    /// Service provider compendium (catalog, pricelist)
    /// </summary>
    public class Cpmpendium
    {

        /// <summary>
        /// Arenal Id of a provider.
        /// </summary>
        public string ProviderId { get; set; }

        /// <summary>
        /// List of <see cref="CompendiumEntry"/> provided by this provider.
        /// </summary>
        public IEnumerable<CompendiumEntry> ServiceList { get; set; }

    }

}
