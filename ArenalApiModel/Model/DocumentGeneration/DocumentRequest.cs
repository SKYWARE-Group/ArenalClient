namespace Skyware.Arenal.Model.DocumentGeneration
{

    /// <summary>
    /// Represents a request for document (form) generation.
    /// </summary>
    public class DocumentRequest
    {

        /// <summary>
        /// Type of the requested documents (Lab referral, etc).
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Requested format of a requested document.
        /// </summary>
        public string DocumentFormat { get; set; } = "PDF";

        /// <summary>
        /// Base64 encoded data for document generation.
        /// </summary>
        public string Data { get; set; }

    }

}
