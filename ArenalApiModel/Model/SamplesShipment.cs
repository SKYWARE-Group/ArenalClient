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
    [Display(ShortName = nameof(L10n.SampleShipment.PlacerIdShortName),
        Name = nameof(L10n.SampleShipment.PlacerIdName),
        Description = nameof(L10n.SampleShipment.PlacerIdDescription),
        ResourceType = typeof(L10n.SampleShipment))]
    public string PlacerId { get; set; }

    /// <summary>
    /// ArenalId of a provider to whom the shipment is assigned.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.ProviderIdShortName),
        Name = nameof(L10n.SampleShipment.ProviderIdName),
        Description = nameof(L10n.SampleShipment.ProviderIdDescription),
        ResourceType = typeof(L10n.SampleShipment))]
    public string ProviderId { get; set; }

    /// <summary>
    /// List of <see cref="Identifier"/> (may be empty).
    /// Examples: Carrier (tracker) ServiceId, Sender's ServiceId, etc.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.IdentifiersShortName),
        Name = nameof(L10n.SampleShipment.IdentifiersName),
        Description = nameof(L10n.SampleShipment.IdentifiersDescription),
        Prompt = nameof(L10n.SampleShipment.IdentifiersPrompt),
        ResourceType = typeof(L10n.SampleShipment))]
    public IEnumerable<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Date and time the shipment is sent.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.SentShortName), 
        Name = nameof(L10n.SampleShipment.SentName),
        Description = nameof(L10n.SampleShipment.SentDescription), 
        Prompt = nameof(L10n.SampleShipment.SentPrompt),
        ResourceType = typeof(L10n.SampleShipment))]
    public DateTime? Sent { get; set; }

    /// <summary>
    /// Notes from the placer.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleShipment.NoteShortName),
        Name = nameof(L10n.SampleShipment.NoteName),
        Description = nameof(L10n.SampleShipment.NoteDescription),
        Prompt = nameof(L10n.SampleShipment.NotePrompt),
        ResourceType = typeof(L10n.SampleShipment))]
    public Note Note { get; set; }

}
