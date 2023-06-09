using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represents a shipment of samples from placer to service provider.
/// </summary>
public class SampleShipment : EntityBase
{

    /// <summary>
    /// ArenalId of the ordering party.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.SampleShipment.PlacerIdShortName),
        Name = nameof(L10n.SampleShipment.SampleShipment.PlacerIdName),
        Description = nameof(L10n.SampleShipment.SampleShipment.PlacerIdDescription),
        ResourceType = typeof(L10n.SampleShipment.SampleShipment))]
    public string PlacerId { get; set; }

    /// <summary>
    /// ArenalId of a provider to whom the shipment is assigned.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.SampleShipment.ProviderIdShortName),
        Name = nameof(L10n.SampleShipment.SampleShipment.ProviderIdName),
        Description = nameof(L10n.SampleShipment.SampleShipment.ProviderIdDescription),
        ResourceType = typeof(L10n.SampleShipment.SampleShipment))]
    public string ProviderId { get; set; }

    /// <summary>
    /// List of <see cref="Identifier"/> (may be empty).
    /// Examples: Carrier (tracker) ServiceId, Sender's ServiceId, etc.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.SampleShipment.IdentifiersShortName),
        Name = nameof(L10n.SampleShipment.SampleShipment.IdentifiersName),
        Description = nameof(L10n.SampleShipment.SampleShipment.IdentifiersDescription),
        Prompt = nameof(L10n.SampleShipment.SampleShipment.IdentifiersPrompt),
        ResourceType = typeof(L10n.SampleShipment.SampleShipment))]
    public IEnumerable<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Date and time the shipment is sent.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.SampleShipment.SentShortName), 
        Name = nameof(L10n.SampleShipment.SampleShipment.SentName),
        Description = nameof(L10n.SampleShipment.SampleShipment.SentDescription), 
        Prompt = nameof(L10n.SampleShipment.SampleShipment.SentPrompt),
        ResourceType = typeof(L10n.SampleShipment.SampleShipment))]
    public DateTime? Sent { get; set; }

    /// <summary>
    /// Notes from the placer.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.SampleShipment.NoteShortName),
        Name = nameof(L10n.SampleShipment.SampleShipment.NoteName),
        Description = nameof(L10n.SampleShipment.SampleShipment.NoteDescription),
        Prompt = nameof(L10n.SampleShipment.SampleShipment.NotePrompt),
        ResourceType = typeof(L10n.SampleShipment.SampleShipment))]
    public Note Note { get; set; }

}
