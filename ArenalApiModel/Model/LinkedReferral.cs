using System;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Reference to a referral or order in an external system.
    /// </summary>
    public class LinkedReferral
    {

        /// <summary>
        /// Identifier in external system.
        /// </summary>
        public Identifier Id { get; set; }

        /// <summary>
        /// Date and time the referral will expire.
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// Note from the placer
        /// </summary>
        public Note Note { get; set; }

    }

}
