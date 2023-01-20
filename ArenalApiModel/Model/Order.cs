﻿using System;
using System.Collections.Generic;


/// <summary>
/// Medical order for laboratory examination or observation.
/// </summary>
public class Order
{

    /// <summary>
    /// The identifier, generated by this system.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Placer's order identifier. This is the identifier generated by the party which places order. It will be returned back as a reference.
    /// </summary>
    public string PlacerId { get; set; }

    /// <summary>
    /// Date and time the order was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Additional orders or referrals, which are part of his order and ares stored and processed in external systems.
    /// </summary>
    public IEnumerable<LinkedReferral> LinkedReferrals { get; set; }

    /// <summary>
    /// Patient
    /// </summary>
    public Patient Patient { get; set; }

    /// <summary>
    /// Array of requested examinations or observations
    /// </summary>
    public List<Service> Sevrices { get; set; }

    /// <summary>
    /// Array of provided samples
    /// </summary>
    public List<Sample> Samples { get; set; }

}

