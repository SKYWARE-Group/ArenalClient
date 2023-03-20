using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represents address of a person or organization. 
    /// </summary>
    public class Address
    {

        /// <summary>
        /// State or Region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Name of the city/town.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Postal code.
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Actual address, e.g. "10 Downing St"
        /// </summary>
        public string StreetAddress { get; set; }

    }

}
