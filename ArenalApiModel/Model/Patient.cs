using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Patient: patient, doctor, etc.
    /// </summary>
    public class Patient : PersonBase
    {

        /// <summary>
        /// Date of birth of the person.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// True if date of birth is exact value, false if is calculated from approximate age.
        /// True by default. If no value is provided, default (true) value will be used.
        /// </summary>
        public bool IsDateOfBirthApproximate { get; set; } = false; 

        /// <summary>
        /// True for males, False for females and Null for other/unknown.
        /// </summary>
        public bool? IsMale { get; set; }

        /// <summary>
        /// Order related files.
        /// </summary>
        public IEnumerable<Attachment> Attachments { get; set; }

    }

}