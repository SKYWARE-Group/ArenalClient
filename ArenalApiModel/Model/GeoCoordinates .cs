using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represent geographic location of an object.
    /// </summary>
    public class GeoCoordinates
    {

        /// <summary>
        /// Geographic latitude (recommended precision is 4 digits after decimal point).
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Geographic longitude (recommended precision is 4 digits after decimal point).
        /// </summary>
        public decimal Longitude { get; set; }

    }

}
