using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represent a contact info, such as email, phone number, etc.
/// </summary>
public class Contact
{

    /// <summary>
    /// Maximum length of <see cref="Value"/> field.
    /// </summary>
    public const int VALUE_MAX_LEN = 80;

    /// <summary>
    /// Type of the contact, according to <see cref="ContactTypes"/>.
    /// Required.
    /// </summary>
    public string Type { get; set; } = ContactTypes.PHONE;

    /// <summary>
    /// Value of the contact, such as 'john@doe.com', etc.
    /// Required.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Contact() { }

    /// <summary>
    /// Shorthand constructor for instantiation with phone number.
    /// </summary>
    /// <param name="phone"></param>
    public Contact(string phone) : this()
    {
        Value = phone;
    }

    /// <summary>
    /// Convenience constructor
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public Contact(string type, string value) : this()
    {
        Type = type;
        Value = value;
    }

}
