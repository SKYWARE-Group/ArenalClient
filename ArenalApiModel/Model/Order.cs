﻿using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Medical order for laboratory examination/observation or service.
    /// </summary>
    public class Order : EntityBase
    {

        /// <summary>
        /// ArenalId of the ordering party.
        /// </summary>
        public string PlacerId { get; set; }

        /// <summary>
        /// ArenalId of the service provider to which this order is intended.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public string ProviderId { get; set; }

        /// <summary>
        /// Placer's order identifier. This is the identifier generated by the party which places the order.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public string PlacerOrderId { get; set; }

        /// <summary>
        /// Filler's order identifier. This is the identifier generated by the party which consumes the order.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public string ProviderOrderId { get; set; }

        /// <summary>
        /// Date and time the order was created.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Date and time the order was modified.
        /// </summary>
        public DateTime? Modified { get; set; }

        /// <summary>
        /// Version (server generated), starts from 0 and increments on every update from the publisher side.
        /// </summary>
        public int Version { get; set; } = 0;

        /// <summary>
        /// Order status, according to <see cref="OrderStatuses"/>.
        /// </summary>
        public string Status { get; set; } = OrderStatuses.FREE;
        
        /// <summary>
        /// Notes from the placer.
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Patient
        /// </summary>
        public Person Patient { get; set; }

        /// <summary>
        /// Additional orders or referrals, which are part of his order and ares stored and processed in external systems.
        /// </summary>
        public IEnumerable<LinkedReferral> LinkedReferrals { get; set; }

        /// <summary>
        /// Array of requested examinations or observations
        /// </summary>
        public IEnumerable<Service> Sevrices { get; set; }

        /// <summary>
        /// Array of provided samples
        /// </summary>
        public IEnumerable<Sample> Samples { get; set; }

        /// <summary>
        /// Order related files.
        /// </summary>
        public IEnumerable<Attachment> Attachments { get; set; }

    }
}

