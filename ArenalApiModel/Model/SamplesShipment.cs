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
        /// ArenalId of the ordering party.
        /// </summary>
        public string PlacerId { get; set; }

        /// <summary>
        /// ArenalId of a provider to whom the shipment is assigned.
        /// </summary>
        public string ProviderId { get; set; }

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
        /// Notes from the placer.
        /// </summary>
        public Note Note { get; set; }

    }

}
