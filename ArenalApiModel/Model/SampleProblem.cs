using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represent a problem or discrepancy, connected with a service.
    /// Preferred coding system is 'org.hl7' with dictionary '0490'
    /// </summary>
    public class SampleProblem
    {

        /// <summary>
        /// Reference to a <see cref="Identifier"/> of a <see cref="Sample"/>
        /// </summary>
        public string SampleId { get; set; }

        /// <summary>
        /// The problem object
        /// </summary>
        public Problem Problem { get; set; }    

    }

}
