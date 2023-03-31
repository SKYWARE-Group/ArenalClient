namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represents address of a person or organization. 
    /// </summary>
    public class Address
    {

        /// <summary>
        /// Two-letter country code, according to ISO 3166-1 alpha 2
        /// </summary>
        public string CountryCode { get; set; }

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
