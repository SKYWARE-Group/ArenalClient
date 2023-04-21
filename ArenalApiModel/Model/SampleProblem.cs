using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represent a problem or discrepancy, connected with a service.
    /// Preferred coding system is 'org.hl7' with dictionary '0490'
    /// </summary>
    public class SampleProblem : Problem
    {

        /// <summary>
        /// Reference to a <see cref="Identifier"/> of a <see cref="Sample"/>
        /// </summary>
        public string SampleId { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SampleProblem() { }

        /// <summary>
        /// Instantiates an object with HL7 coded sample problem
        /// </summary>
        public SampleProblem(string problemCode, string sampleId) : this()
        {
            Identifier = new Identifier("org.lh7", "0490", problemCode);
            SampleId = sampleId;
        }

    }

}
