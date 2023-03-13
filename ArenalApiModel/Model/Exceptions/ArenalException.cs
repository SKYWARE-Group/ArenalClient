using System;
using System.Text;

namespace Skyware.Arenal.Model.Exceptions
{

    /// <summary>
    /// Represents typed Arenal exception
    /// </summary>
    public class ArenalException : Exception
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="error"></param>
        public ArenalException(int? httpStatusCode, ArenalError error)
        {
            HttpStatusCode = httpStatusCode;
            Error = error;
        }

        /// <summary>
        /// Transport level status code.
        /// </summary>
        public int? HttpStatusCode { get; set; }

        /// <summary>
        /// Arenal error.
        /// </summary>
        public ArenalError Error { get; set; }

        /// <summary>
        /// Consolidated message - both from the transport and from the Arenal.
        /// </summary>
        /// <returns></returns>
        public string CombinedMessage()
        {
            StringBuilder builder = new StringBuilder();
            if (Error != null && !string.IsNullOrWhiteSpace(Error.Message)) builder.Append(Error.Message + Environment.NewLine);
            if (!string.IsNullOrWhiteSpace(this.Message)) builder.Append(this.Message);
            return builder.ToString();
        }

    }

}
