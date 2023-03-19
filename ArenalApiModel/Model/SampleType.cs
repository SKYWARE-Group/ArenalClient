using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Sample type (laboratory)
    /// </summary>
    public class SampleType
    {

        /// <summary>
        /// Identifier of the sample type.
        /// </summary>
        public Identifier TypeId { get; set; }

        /// <summary>
        /// Identifier of additive.
        /// </summary>
        public Identifier AditiveId { get; set; }

        /// <summary>
        /// Additional identifiers, not defined in <see cref="TypeId"/> and <see cref="AditiveId"/>.
        /// </summary>
        public IEnumerable<Identifier> AlternativeIdentifiers { get; set; }

        /// <summary>
        /// Name of the sample type as it is known by the placer or provider.
        /// </summary>
        public string Name { get; set; }

    }

}
