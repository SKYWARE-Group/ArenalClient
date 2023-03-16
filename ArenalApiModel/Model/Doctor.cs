using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represent a doctor.
    /// </summary>
    public class Doctor : PersonBase 
    {

        /// <summary>
        /// Title, such as D-r, Prof., etc.
        /// </summary>
        public string Title { get; set; }

    }
}
