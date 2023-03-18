using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Base class for patients, doctors, etc.
    /// </summary>
    public abstract class PersonBase
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
        /// List of phone numbers, emails, etc.
        /// </summary>
        public IEnumerable<Contact> Contacts { get; set; }

    }

}
