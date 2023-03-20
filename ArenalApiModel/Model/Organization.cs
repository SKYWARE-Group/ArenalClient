using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Arenal participant (Medical Center, Laboratory, Health Insurance Fund, etc.).
    /// </summary>
    public class Organization : EntityBase
    {

        /// <summary>
        /// List of <see cref="Identifier"/> (may be empty).
        /// </summary>
        public IEnumerable<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Official name of the organization such as 'Precisio Medical Laboratories Inc.'.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short name of the provider or brand name, e.g. 'Precisio'.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Approved roles in Arenal.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

    }

}
