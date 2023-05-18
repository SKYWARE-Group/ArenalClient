using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represent a diagnosis, implies dagger-asterisk system of a ICD.
/// </summary>
public class Diagnosis
{

    /// <summary>
    /// The primary (dagger) code of a diagnosis.
    /// </summary>
    [Display(GroupName = nameof(L10n.Diagnosis.PrimaryCodeGroupName),
        ShortName = nameof(L10n.Diagnosis.PrimaryCodeShortName),
        Name = nameof(L10n.Diagnosis.PrimaryCodeName),
        Description = nameof(L10n.Diagnosis.PrimaryCodeDescription),
        ResourceType = typeof(L10n.Diagnosis))]
    public Identifier PrimaryCode { get; set; }

    /// <summary>
    /// The clarification/complication (asterisk) code.
    /// </summary>
    [Display(GroupName = nameof(L10n.Diagnosis.OptionalCodeGroupName),
        ShortName = nameof(L10n.Diagnosis.OptionalCodeShortName),
        Name = nameof(L10n.Diagnosis.OptionalCodeName),
        Description = nameof(L10n.Diagnosis.OptionalCodeDescription),
        ResourceType = typeof(L10n.Diagnosis))]
    public Identifier OptionalCode { get; set; }

    /// <summary>
    /// The diagnosis name, according to local language.
    /// </summary>
    [Display(GroupName = nameof(L10n.Diagnosis.NameGroupName),
        ShortName = nameof(L10n.Diagnosis.NameShortName),
        Name = nameof(L10n.Diagnosis.NameName),
        Description = nameof(L10n.Diagnosis.NameDescription),
        Prompt = nameof(L10n.Diagnosis.NamePrompt),
        ResourceType = typeof(L10n.Diagnosis))]
    public string Name { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Diagnosis () { }

    /// <summary>
    /// Instantiates ICD-10 coded diagnosis.
    /// </summary>
    /// <param name="priamryCode"></param>
    /// <param name="optionalCode"></param>
    /// <param name="name"></param>
    public Diagnosis(string priamryCode, string optionalCode = null, string name = null) : this()
    {
        if (!string.IsNullOrWhiteSpace(priamryCode)) PrimaryCode = new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, priamryCode);
        if (!string.IsNullOrWhiteSpace(optionalCode)) OptionalCode = new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, optionalCode);
        Name = name;
    }

}
