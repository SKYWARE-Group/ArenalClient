namespace Skyware.Arenal.Model.Exceptions
{

    /// <summary>
    /// Represents Arenal error.
    /// </summary>
    public class ArenalError
    {

        /// <summary>
        /// Status code, according to Arenal specification.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error description.
        /// </summary>
        public string Message { get; set; }

    }
}
