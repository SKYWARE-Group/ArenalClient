using FluentValidation.Results;
using Skyware.Arenal.Validation;
using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model;

/// <summary>
/// Patient.
/// </summary>
public class Patient : PersonBase
{

    private static PatientValidator _validator;

    /// <summary>
    /// Minimal allowed date of birth of the patient.
    /// </summary>
    public  static readonly DateTime MIN_DOB = new DateTime(1900, 1, 1).ToUniversalTime();

    /// <summary>
    /// Date of birth of the person (UTC). Optional. 
    /// When provided must be between <see cref="MIN_DOB"/> and current date and time (UTC).
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Date of birth of the person (Local date and time).
    /// </summary>
    public DateTime? LocalDateOfBirth => DateOfBirth?.ToLocalTime();

    /// <summary>
    /// True if date of birth is exact value, false if is calculated from approximate age.
    /// Required, true by default.
    /// </summary>
    public bool ExactDoB { get; set; } = true; 

    /// <summary>
    /// True for males, False for females and Null for other/unknown.
    /// </summary>
    public bool? IsMale { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Patient() { }

    /// <summary>
    /// Shorthand constructor.
    /// </summary>
    /// <param name="givenNme"></param>
    /// <param name="familyName"></param>
    /// <param name="isMale"></param>
    /// <param name="born"></param>
    public Patient(string givenNme, string familyName, bool? isMale = null, DateTime? born = null) : this() { 
        GivenName = givenNme;
        FamilyName = familyName;
        IsMale = isMale;
        if (born is not null) DateOfBirth = born;
    }

    /// <summary>
    /// Creates new <see cref="Order"/> and set this instance as a <see cref="Order.Patient"/>.
    /// </summary>
    /// <param name="wokrflow"></param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public Order CreateOrder(string wokrflow, string placerId, string providerId) =>
        new () { Workflow = wokrflow, PlacerId = placerId, ProviderId = providerId, Patient = this };
    

    /// <summary>
    /// Convenience method for safely addition of a phone number as a contact.
    /// </summary>
    /// <param name="phone">The phone number</param>
    /// <returns></returns>
    public virtual Patient AddPhone(string phone)
    {
        (Contacts ??= new List<Contact>()).Add(new Contact(ContactTypes.PHONE, phone));
        return this;
    }

    /// <summary>
    /// Convenience method for safely addition of an email as a contact.
    /// </summary>
    /// <param name="email">The email address</param>
    /// <returns></returns>
    public virtual Patient AddEmail(string email)
    {
        (Contacts ??= new List<Contact>()).Add(new Contact(ContactTypes.EMAIL, email));
        return this;
    }

    /// <summary>
    /// Convenience method for safely addition of an identifier.
    /// </summary>
    /// <param name="authority">Authority of the identifier</param>
    /// <param name="value">The identifier</param>
    /// <param name="dictionary">(Optional) Authority's dictionary</param>
    /// <returns></returns>
    public virtual Patient AddIdentifier(string authority, string value, string dictionary = null)
    {
        (Identifiers ??= new List<Identifier>()).Add(new Identifier(authority, dictionary, value));
        return this;
    }

    public ValidationResult Validate()
    {
        return (_validator ??= new PatientValidator()).Validate(this);
    }

}