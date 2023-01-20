using System;

/// <summary>
/// Patient, the subject of an order or result.
/// </summary>
public class Patient
{

    /// <summary>
    /// Scheme of the personal identifier such as 'us.ssn' 'bg.egn', 'mk.jmbg', etc.
    /// Null is used for patients that can't be unidentified with well-known identifier.
    /// </summary>
    public string IdScheme { get; set; }

    /// <summary>
    /// Personal identifier such as ЕГН, ЕМБГ, SSN, etc. 
    /// Null is used for unidentified patients.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Placer's identifier of the patient. 
    /// </summary>
    public string PlacerId { get; set; }

    /// <summary>
    /// Given (first) name of the patient.
    /// </summary>
    public string GivenName { get; set; }

    /// <summary>
    /// Middle (second) name of the patient.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// Family name (surname) of the patient.
    /// </summary>
    public string FamilyName { get; set; }

    /// <summary>
    /// Date of birth of the patient.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// True if date of birth is exact value, false if is calculated from approximate age.
    /// True by default. If no value is provided, default (true) value will be used.
    /// </summary>
    public bool? IsDateOfBirthApproximate { get; set; }

    /// <summary>
    /// True for males, False for females and Null for other/unknown.
    /// </summary>
    public bool? IsMale { get; set; }

}