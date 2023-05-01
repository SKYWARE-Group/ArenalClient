using FluentValidation.Results;
using Skyware.Arenal.Validation;
using System;

namespace Skyware.Arenal.Model;


/// <summary>
/// Identifier.
/// </summary>
public class Identifier : IEquatable<Identifier>
{
    private static IdentifierValidator _validator;

    /// <summary>
    /// Authority/Realm/System of the identifier such as 'org.loinc', 'org.snomed' etc.
    /// Mandatory. Use 'local' for your own identifiers.
    /// </summary>
    public string Authority { get; set; }

    /// <summary>
    /// Dictionary (value set) for given authority, such as HL7 table number, etc.
    /// Optional.
    /// </summary>
    public string Dictionary { get; set; }

    /// <summary>
    /// Identifier value such as 'ABC-123', 'BLD', etc. 
    /// Mandatory.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Identifier() { }

    /// <summary>
    /// Creates an instance with local authority and no dictionary.
    /// </summary>
    public Identifier(string value) : this()
    {
        Authority = Authorities.LOCAL;
        Value = value;
    }

    /// <summary>
    /// Creates an object with authority and dictionary.
    /// </summary>
    public Identifier(string authority, string dictionary, string value) : this()
    {
        Authority = authority;
        Dictionary = dictionary;
        Value = value;
    }

    /// <summary>
    /// Check if two identifiers are semantically equal.
    /// </summary>
    /// <remarks>
    /// If both identifiers are null, they are considered equal.
    /// </remarks>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Identifier other) => GetHashCode() == other?.GetHashCode();

    /// <inheritdoc/>
    public override int GetHashCode() => $"{Authority.EmptyIfNull().ToLower()}{Dictionary.EmptyIfNull().ToLower()}{Value.EmptyIfNull().ToLower()}".GetHashCode();

    /// <inheritdoc/>
    public static bool operator ==(Identifier a, Identifier b) => (a is null && b is null) || (a?.GetHashCode().Equals(b?.GetHashCode()) ?? false);

    /// <inheritdoc/>
    public static bool operator !=(Identifier a, Identifier b) => (a is null && b is not null) || !(a?.GetHashCode().Equals(b?.GetHashCode()) ?? false);

    /// <<inheritdoc/>
    public override bool Equals(object obj)
    {
        return this == (Identifier)obj;
    }

    public ValidationResult Validate()
    {
        return (_validator ??= new IdentifierValidator()).Validate(this);
    }

}
