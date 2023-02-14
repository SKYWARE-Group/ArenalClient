using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Service provider, e.g. laboratory.
    /// </summary>
    public class Provider : EntityBase 
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

    }

}
