﻿using System.ComponentModel.DataAnnotations;
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
    [Display(GroupName = nameof(L10n.Diagnosis.Diagnosis.PrimaryCodeGroupName),
        ShortName = nameof(L10n.Diagnosis.Diagnosis.PrimaryCodeShortName),
        Name = nameof(L10n.Diagnosis.Diagnosis.PrimaryCodeName),
        Description = nameof(L10n.Diagnosis.Diagnosis.PrimaryCodeDescription),
        ResourceType = typeof(L10n.Diagnosis.Diagnosis))]
    public Identifier PrimaryCode { get; set; }

    /// <summary>
    /// The clarification/complication (asterisk) code.
    /// </summary>
    [Display(GroupName = nameof(L10n.Diagnosis.Diagnosis.OptionalCodeGroupName),
        ShortName = nameof(L10n.Diagnosis.Diagnosis.OptionalCodeShortName),
        Name = nameof(L10n.Diagnosis.Diagnosis.OptionalCodeName),
        Description = nameof(L10n.Diagnosis.Diagnosis.OptionalCodeDescription),
        ResourceType = typeof(L10n.Diagnosis.Diagnosis))]
    public Identifier OptionalCode { get; set; }

    /// <summary>
    /// The diagnosis name, according to local language.
    /// </summary>
    [Display(GroupName = nameof(L10n.Diagnosis.Diagnosis.NameGroupName),
        ShortName = nameof(L10n.Diagnosis.Diagnosis.NameShortName),
        Name = nameof(L10n.Diagnosis.Diagnosis.NameName),
        Description = nameof(L10n.Diagnosis.Diagnosis.NameDescription),
        Prompt = nameof(L10n.Diagnosis.Diagnosis.NamePrompt),
        ResourceType = typeof(L10n.Diagnosis.Diagnosis))]
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
