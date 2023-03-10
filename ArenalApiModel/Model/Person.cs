using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Person: patient, doctor, etc.
    /// </summary>
    public class Person
    {

        /// <summary>
        /// List of <see cref="Identifier"/> (may be empty).
        /// </summary>
        public IEnumerable<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Given (first) name of the person.
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Middle (second) name of the person.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Family name (surname) of the person.
        /// </summary>
        public string FamilyName { get; set; }

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
        /// List of phone numbers, emails, etc.
        /// </summary>
        public IEnumerable<Contact> Contacts { get; set; }

        /// <summary>
        /// Order related files.
        /// </summary>
        public IEnumerable<Attachment> Attachments { get; set; }

    }

}