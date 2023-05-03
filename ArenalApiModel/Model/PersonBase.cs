using System.Collections.Generic;
using System.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Base class for patients, doctors, etc.
/// </summary>
public abstract class PersonBase
{
    /// <summary>
    /// Minimum length of full name of the person.
    /// Value is calculated on <see cref="GivenName"/> + <see cref="MiddleName"/> + <see cref="FamilyName"/>.
    /// </summary>
    public const int NAMES_MIN_LEN = 2;

    /// <summary>
    /// Maximum length of full name of the person.
    /// Value is calculated on <see cref="GivenName"/> + <see cref="MiddleName"/> + <see cref="FamilyName"/>.
    /// </summary>
    public const int NAMES_MAX_LEN = 100;

    /// <summary>
    /// Maximum allowed number of identifiers.
    /// </summary>
    public const int MAX_IDENTIFIERS = 10;

    /// <summary>
    /// Maximum allowed number of contacts.
    /// </summary>
    public const int MAX_CONTACTS = 10;

    /// <summary>
    /// List of <see cref="Identifier"/> (may be empty).
    /// </summary>
    public IList<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Given (first) name of the person.
    /// </summary>
    public string GivenName { get; set; }

    /// <summary>
    /// Middle (second) name of the person.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// Family name (surname) of the person.
    /// </summary>
    public string FamilyName { get; set; }

    /// <summary>
    /// Full name of the person.
    /// </summary>
    public string FullName =>
        string.Join(
            " ",
            (new string[] { GivenName.EmptyIfNull(), MiddleName.EmptyIfNull(), FamilyName.EmptyIfNull() }).Where(x => !string.IsNullOrWhiteSpace(x))
        );

    /// <summary>
    /// List of phone numbers, emails, etc.
    /// </summary>
    public IList<Contact> Contacts { get; set; }


}
