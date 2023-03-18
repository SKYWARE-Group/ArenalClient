using System.Net.Http.Headers;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Identifier.
    /// </summary>
    public class Identifier
    {

        /// <summary>
        /// Authority/Realm/System of the identifier such as 'us.ssn' 'bg.egn', 'org.loinc', 'org.snomed' etc.
        /// Mandatory. Use 'local' for your own identifiers.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Dictionary (value set) for given authority, such as HL7 table number, etc.
        /// Optional.
        /// </summary>
        public string Dictionary { get; set; }

        /// <summary>
        /// Identifier value such as 'ABC-123', 'BLD', etc. 
        /// Mandatory.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Identifier()
        {

        }

        /// <summary>
        /// Shorthand constructor.
        /// </summary>
        public Identifier(string authority, string dictionary, string value) : this()
        {
            Authority = authority;
            Dictionary = dictionary;
            Value = value;
        }

    }

}
