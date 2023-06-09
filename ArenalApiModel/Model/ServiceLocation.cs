using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skyware.Arenal.Model;


/// <summary>
/// Physician office, sample collection point, etc. service location.
/// </summary>
[DisplayColumn(nameof(Name))]
public class ServiceLocation
{

    /// <summary>
    /// Id of the location according to the provider.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.ServiceLocation.LocationIdShortName),
        Name = nameof(L10n.ServiceLocation.ServiceLocation.LocationIdName),
        Description = nameof(L10n.ServiceLocation.ServiceLocation.LocationIdDescription),
        Prompt = nameof(L10n.ServiceLocation.ServiceLocation.LocationIdPrompt),
        ResourceType = typeof(L10n.ServiceLocation.ServiceLocation))]
    public string LocationId { get; set; }

    /// <summary>
    /// Friendly name, such as 'Main office'.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.ServiceLocation.NameShortName),
        Name = nameof(L10n.ServiceLocation.ServiceLocation.NameName),
        Description = nameof(L10n.ServiceLocation.ServiceLocation.NameDescription),
        Prompt = nameof(L10n.ServiceLocation.ServiceLocation.NamePrompt),
        ResourceType = typeof(L10n.ServiceLocation.ServiceLocation))]
    public string Name { get; set; }

    /// <summary>
    /// Notes to the location, such as 'Call before visit'.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.ServiceLocation.NoteShortName),
        Name = nameof(L10n.ServiceLocation.ServiceLocation.NoteName),
        Description = nameof(L10n.ServiceLocation.ServiceLocation.NoteDescription),
        Prompt = nameof(L10n.ServiceLocation.ServiceLocation.NotePrompt),
        ResourceType = typeof(L10n.ServiceLocation.ServiceLocation))]
    public Note Note { get; set; }

    /// <summary>
    /// Address of the location.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.ServiceLocation.AddressShortName),
        Name = nameof(L10n.ServiceLocation.ServiceLocation.AddressName),
        Description = nameof(L10n.ServiceLocation.ServiceLocation.AddressDescription),
        Prompt = nameof(L10n.ServiceLocation.ServiceLocation.AddressPrompt),
        ResourceType = typeof(L10n.ServiceLocation.ServiceLocation))]
    public Address Address { get; set; }

    /// <summary>
    /// List of contact info, such as emails, phones, etc.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.ServiceLocation.ContactsShortName),
        Name = nameof(L10n.ServiceLocation.ServiceLocation.ContactsName),
        Description = nameof(L10n.ServiceLocation.ServiceLocation.ContactsDescription),
        Prompt = nameof(L10n.ServiceLocation.ServiceLocation.ContactsPrompt),
        ResourceType = typeof(L10n.ServiceLocation.ServiceLocation))]
    public IEnumerable<Contact> Contacts { get; set; }

}
