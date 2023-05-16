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
    public string LocationId { get; set; }

    /// <summary>
    /// Friendly name, such as 'Main office'.
    /// </summary>
    [Display(ShortName = "Service Location", Name = "Service Location",
        Description = $"Friendly name of the service location.")]
    public string Name { get; set; }

    /// <summary>
    /// Notes to the location, such as 'Call before visit'.
    /// </summary>
    [Display(ShortName = "Note", Name = "Note",
        Description = $"Note to the location, such as 'Call before visit'.")]
    public Note Note { get; set; }

    /// <summary>
    /// Address of the location.
    /// </summary>
    [Display(ShortName = "Address", Name = "Address",
        Description = $"Address of the location for the service.")]
    public Address Address { get; set; }

    /// <summary>
    /// List of contact info, such as emails, phones, etc.
    /// </summary>
    [Display(ShortName = "Contacts", Name = "Contacts",
        Description = $"Contacts applicable with the service location.")]
    public IEnumerable<Contact> Contacts { get; set; }

}
