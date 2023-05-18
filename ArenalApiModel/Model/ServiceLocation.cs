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
    [Display(ShortName = nameof(L10n.ServiceLocation.LocationIdShortName),
        Name = nameof(L10n.ServiceLocation.LocationIdName),
        Description = nameof(L10n.ServiceLocation.LocationIdDescription),
        Prompt = nameof(L10n.ServiceLocation.LocationIdPrompt),
        ResourceType = typeof(L10n.ServiceLocation))]
    public string LocationId { get; set; }

    /// <summary>
    /// Friendly name, such as 'Main office'.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.NameShortName),
        Name = nameof(L10n.ServiceLocation.NameName),
        Description = nameof(L10n.ServiceLocation.NameDescription),
        Prompt = nameof(L10n.ServiceLocation.NamePrompt),
        ResourceType = typeof(L10n.ServiceLocation))]
    public string Name { get; set; }

    /// <summary>
    /// Notes to the location, such as 'Call before visit'.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.NoteShortName),
        Name = nameof(L10n.ServiceLocation.NoteName),
        Description = nameof(L10n.ServiceLocation.NoteDescription),
        Prompt = nameof(L10n.ServiceLocation.NotePrompt),
        ResourceType = typeof(L10n.ServiceLocation))]
    public Note Note { get; set; }

    /// <summary>
    /// Address of the location.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.AddressShortName),
        Name = nameof(L10n.ServiceLocation.AddressName),
        Description = nameof(L10n.ServiceLocation.AddressDescription),
        Prompt = nameof(L10n.ServiceLocation.AddressPrompt),
        ResourceType = typeof(L10n.ServiceLocation))]
    public Address Address { get; set; }

    /// <summary>
    /// List of contact info, such as emails, phones, etc.
    /// </summary>
    [Display(ShortName = nameof(L10n.ServiceLocation.ContactsShortName),
        Name = nameof(L10n.ServiceLocation.ContactsName),
        Description = nameof(L10n.ServiceLocation.ContactsDescription),
        Prompt = nameof(L10n.ServiceLocation.ContactsPrompt),
        ResourceType = typeof(L10n.ServiceLocation))]
    public IEnumerable<Contact> Contacts { get; set; }

}
