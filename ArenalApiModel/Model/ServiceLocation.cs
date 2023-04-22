using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Physician office, sample collection point, etc. service location.
/// </summary>
public class ServiceLocation
{

    /// <summary>
    /// Id of the location according to the provider.
    /// </summary>
    public string LocationId { get; set; }

    /// <summary>
    /// Friendly name, such as 'Main office'.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Notes to the location, such as 'Call before visit'.
    /// </summary>
    public Note Note { get; set; }

    /// <summary>
    /// Address of the location.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// List of contact info, such as emails, phones, etc.
    /// </summary>
    public IEnumerable<Contact> Contacts { get; set; }

}
