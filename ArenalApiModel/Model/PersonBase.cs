using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Base class for patients, PersonBases, etc.
/// </summary>
[DisplayColumn(nameof(FamilyName))]
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
    [Display(GroupName = nameof(L10n.PersonBase.PersonBase.IdentifiersGroupName),
        ShortName = nameof(L10n.PersonBase.PersonBase.IdentifiersShortName),
        Name = nameof(L10n.PersonBase.PersonBase.IdentifiersName),
        Description = nameof(L10n.PersonBase.PersonBase.IdentifiersDescription),
        Prompt = nameof(L10n.PersonBase.PersonBase.IdentifiersPrompt),
        ResourceType = typeof(L10n.PersonBase.PersonBase))]
    public IList<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Given (first) name of the person.
    /// </summary>
    [Display(GroupName = nameof(L10n.PersonBase.PersonBase.GivenNameGroupName),
        ShortName = nameof(L10n.PersonBase.PersonBase.GivenNameShortName),
        Name = nameof(L10n.PersonBase.PersonBase.GivenNameName),
        Description = nameof(L10n.PersonBase.PersonBase.GivenNameDescription),
        Prompt = nameof(L10n.PersonBase.PersonBase.GivenNamePrompt),
        ResourceType = typeof(L10n.PersonBase.PersonBase))]
    public string GivenName { get; set; }

    /// <summary>
    /// Middle (second) name of the person.
    /// </summary>
    [Display(GroupName = nameof(L10n.PersonBase.PersonBase.MiddleNameGroupName),
        ShortName = nameof(L10n.PersonBase.PersonBase.MiddleNameShortName),
        Name = nameof(L10n.PersonBase.PersonBase.MiddleNameName),
        Description = nameof(L10n.PersonBase.PersonBase.MiddleNameDescription),
        Prompt = nameof(L10n.PersonBase.PersonBase.MiddleNamePrompt),
        ResourceType = typeof(L10n.PersonBase.PersonBase))]
    public string MiddleName { get; set; }

    /// <summary>
    /// Family name (surname) of the person.
    /// </summary>
    [Display(GroupName = nameof(L10n.PersonBase.PersonBase.FamilyNameGroupName),
        ShortName = nameof(L10n.PersonBase.PersonBase.FamilyNameShortName),
        Name = nameof(L10n.PersonBase.PersonBase.FamilyNameName),
        Description = nameof(L10n.PersonBase.PersonBase.FamilyNameDescription),
        Prompt = nameof(L10n.PersonBase.PersonBase.FamilyNamePrompt),
        ResourceType = typeof(L10n.PersonBase.PersonBase))]
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
    [Display(GroupName = nameof(L10n.PersonBase.PersonBase.ContactsGroupName),
        ShortName = nameof(L10n.PersonBase.PersonBase.ContactsBaseShortName),
        Name = nameof(L10n.PersonBase.PersonBase.ContactsName),
        Description = nameof(L10n.PersonBase.PersonBase.ContactsDescription),
        Prompt = nameof(L10n.PersonBase.PersonBase.ContactsPrompt),
        ResourceType = typeof(L10n.PersonBase.PersonBase))]
    public IList<Contact> Contacts { get; set; }


}
