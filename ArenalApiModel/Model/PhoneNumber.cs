namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Phone number of a person or organization.
    /// </summary>
    public class PhoneNumber
    {

        /// <summary>
        /// Optional, type of phone numbers, e.g. 'mobile'.
        /// </summary>
        public string PhoneType { get; set; }

        /// <summary>
        /// The phone number.
        /// </summary>
        public string Value { get; set; }

    }

}
