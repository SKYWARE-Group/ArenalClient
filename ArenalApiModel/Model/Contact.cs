using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represent a contact info, such as email, phone number, etc.
/// </summary>
[DisplayColumn(nameof(Type))]
public class Contact : IEquatable<Contact>
{

    /// <summary>
    /// Maximum length of <see cref="Value"/> field.
    /// </summary>
    public const int VALUE_MAX_LEN = 80;

    /// <summary>
    /// Type of the contact, according to <see cref="ContactTypes"/>.
    /// Required.
    /// </summary>
    [Display(GroupName = nameof(L10n.Contact.ContactGroupName),
        ShortName = nameof(L10n.Contact.ContactTypeShortName),
        Name = nameof(L10n.Contact.ContactTypeName),
        Description = nameof(L10n.Contact.ContactTypeDescription),
        Prompt = nameof(L10n.Contact.ContactTypePrompt),
        ResourceType = typeof(L10n.Contact))]
    public string Type { get; set; } = ContactTypes.PHONE;

    /// <summary>
    /// Value of the contact, such as 'john@doe.com', etc.
    /// Required.
    /// </summary>
    [Display(GroupName = nameof(L10n.Contact.ContactGroupName),
        ShortName = nameof(L10n.Contact.ContactValueShortName),
        Name = nameof(L10n.Contact.ContactValueName),
        Description = nameof(L10n.Contact.ContactValueDescription),
        Prompt = nameof(L10n.Contact.ContactValuePrompt),
        ResourceType = typeof(L10n.Contact))]
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

    /// <summary>
    /// Compares 2 objects of type <see cref="Contact"/> for equality 
    /// for the objects to be equal you need do have same Type and same Value
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Contact other)
    {
        if (other == null) return false;

        return string.Equals(Type, other.Type, StringComparison.OrdinalIgnoreCase)
            && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => $"{Type.EmptyIfNull().ToLower()}{Value.EmptyIfNull().ToLower()}".GetHashCode();

}
