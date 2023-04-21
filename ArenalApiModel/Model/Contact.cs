namespace Skyware.Arenal.Model;


/// <summary>
/// Represent a contact info, such as email, phone number, etc.
/// </summary>
public class Contact
{

    /// <summary>
    /// Type of the contact, according to <see cref="ContactTypes"/>.
    /// </summary>
    public string Type { get; set; } = ContactTypes.PHONE;

    /// <summary>
    /// Value of the contact, such as 'john@doe.com', etc.
    /// </summary>
    public string Value { get; set; }

}
