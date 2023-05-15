using FluentValidation;
using FluentValidation.Results;
using Skyware.Arenal.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Skyware.Arenal.Model;


/// <summary>
/// Service (examination).
/// </summary>
public class Service : IEquatable<Service>
{

    private static ServiceValidator _validator;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Service() { }

    /// <summary>
    /// Instantiates a Service for laboratory examination, coded with Loinc.
    /// </summary>
    public Service(string loincCode, string name = null, string note = null) : this()
    {
        if (!string.IsNullOrWhiteSpace(loincCode)) ServiceId = new Identifier(Authorities.LOINC, null, loincCode);
        Name = name;
        if (!string.IsNullOrWhiteSpace(note)) Note = new Note(note);
    }

    /// <summary>
    /// Identifier of a ordered examination or service.
    /// </summary>
    public Identifier ServiceId { get; set; }

    /// <summary>
    /// Additional identifiers, not defined in <see cref="ServiceId"/>.
    /// </summary>
    public IList<Identifier> AlternateIdentifiers { get; set; }

    /// <summary>
    /// Name of the service, according to the placer (optional).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Notes from the placer.
    /// </summary>
    public Note Note { get; set; }

    /// <summary>
    /// Ordering value, according to the provider's sorting.
    /// </summary>
    public int? Rank { get; set; }

    /// <summary>
    /// List of problems reported by the provider.
    /// </summary>
    public IList<Problem> Problems { get; set; }

    /// <summary>
    /// Services are equal if and only if their primary identifiers are semantically equal.
    /// </summary>
    /// <remarks>
    /// If both primary identifiers are null, services are considered equal.
    /// </remarks>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Service other) => other is not null && ServiceId == other.ServiceId;

    /// <inheritdoc/>
    public override int GetHashCode() => ServiceId.GetHashCode();

    /// <summary>
    /// Safe method to add an alternate identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public Service AddAlternateIdentifier(Identifier identifier)
    {
        (AlternateIdentifiers ??= new List<Identifier>()).Add(identifier);
        return this;
    }

    /// <summary>
    /// Safe method to add an alternate identifier.
    /// </summary>
    /// <param name="authority"></param>
    /// <param name="dictinary"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Service AddAlternateIdentifier(string authority, string dictinary, string value)
    {
        (AlternateIdentifiers ??= new List<Identifier>()).Add(new Identifier(authority, dictinary, value));
        return this;
    }

    /// <summary>
    /// Safe method to add a problem.
    /// </summary>
    /// <param name="problem"></param>
    /// <returns></returns>
    public Service AddProblem(Problem problem)
    {
        (Problems ??= new List<Problem>()).Add(problem);
        return this;
    }

    /// <summary>
    /// Safe method to add a problem.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Service AddProblem(string message)
    {
        (Problems ??= new List<Problem>()).Add(new Problem(new Identifier("0"), message));
        return this;
    }

    /// <summary>
    /// Performs validation against business rules.
    /// </summary>
    /// <returns></returns>
    public ValidationResult Validate()
    {
        return (_validator ??= new ServiceValidator()).Validate(this);
    }


}

