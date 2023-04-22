using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Patient: patient, doctor, etc.
/// </summary>
public class Patient : PersonBase
{

    /// <summary>
    /// Date of birth of the person.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// True if date of birth is exact value, false if is calculated from approximate age.
    /// True by default. If no value is provided, default (true) value will be used.
    /// </summary>
    public bool IsDateOfBirthApproximate { get; set; } = false; 

    /// <summary>
    /// True for males, False for females and Null for other/unknown.
    /// </summary>
    public bool? IsMale { get; set; }

    /// <summary>
    /// Order related files.
    /// </summary>
    public IEnumerable<Attachment> Attachments { get; set; }

    public Patient() { }

    public Patient(string givenNme, string familyName, bool? isMale = null, DateTime? born = null) : this() { 
        GivenName = givenNme;
        FamilyName = familyName;
        IsMale = isMale;
        if (born is not null) DateOfBirth = born;
    }

    public Order CreateOrder(string wokrflow, string providerId)
    {
        return new Order() { Workflow = wokrflow, ProviderId = providerId, Patient = this };
    }


}