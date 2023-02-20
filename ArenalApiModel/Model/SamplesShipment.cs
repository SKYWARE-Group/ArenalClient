using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represents a shipment of samples from placer to service provider.
    /// </summary>
    public class SamplesShipment : EntityBase
    {

        /// <summary>
        /// List of <see cref="Identifier"/> (may be empty).
        /// Examples: Carrier (tracker) Id, Sender's Id, etc.
        /// </summary>
        public IEnumerable<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Date and time the shipment is sent.
        /// </summary>
        public DateTime? Sent { get; set; }

        /// <summary>
        /// ArenalId of a provider to whom the shipment is assigned.
        /// </summary>
        public string DestinationId { get; set; }

    }

}
