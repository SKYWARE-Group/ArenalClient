﻿using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Medical order for laboratory examination or observation.
    /// </summary>
    public class Order : EntityBase
    {

        /// <summary>
        /// Placer's order identifier. This is the identifier generated by the party which places the order.
        /// Optional.
        /// </summary>
        public string PlacerOrderId { get; set; }

        /// <summary>
        /// Filler's order identifier. This is the identifier generated by the party which consumes the order.
        /// Optional.
        /// </summary>
        public string FillerOrderId { get; set; }

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
        /// ArenalId of the service provider to which this order is intended.
        /// Optional.
        /// </summary>
        public string DestinationId { get; set; }

        /// <summary>
        /// Patient
        /// </summary>
        public Patient Patient { get; set; }

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
        /// Notes from the placer.
        /// </summary>
        public string Notes { get; set; }

    }
}

