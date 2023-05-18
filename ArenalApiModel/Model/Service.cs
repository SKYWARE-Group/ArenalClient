using Skyware.Arenal.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Skyware.Arenal.Model;


/// <summary>
/// Service (examination).
/// </summary>
public class Service : IEquatable<Service>
{

    /// <summary>
    /// Maximum length of the service name.
    /// </summary>
    public const int NAME_MAX_LEN = 200;

    /// <summary>
    /// Maximum allowed number of alternate identifies.
    /// </summary>
    public const int ALTERNATE_IDENTIFIERS_MAX = 10;

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
    [Display(ShortName = nameof(L10n.Service.ServiceIdShortName),
        Name = nameof(L10n.Service.ServiceIdName),
        Description = nameof(L10n.Service.ServiceIdDescription),
        Prompt = nameof(L10n.Service.ServiceIdPrompt),
        ResourceType = typeof(L10n.Service))]
    public Identifier ServiceId { get; set; }

    /// <summary>
    /// Additional identifiers, not defined in <see cref="ServiceId"/>.
    /// </summary>
    [Display(ShortName = nameof(L10n.Service.AlternateIdentifiersShortName),
        Name = nameof(L10n.Service.AlternateIdentifiersName),
        Description = nameof(L10n.Service.AlternateIdentifiersDescription),
        Prompt = nameof(L10n.Service.AlternateIdentifiersPrompt),
        ResourceType = typeof(L10n.Service))]
    public IList<Identifier> AlternateIdentifiers { get; set; }

    /// <summary>
    /// Name of the service, according to the placer (optional).
    /// </summary>
    [Display(ShortName = nameof(L10n.Service.NameShortName),
        Name = nameof(L10n.Service.NameName),
        Description = nameof(L10n.Service.NameDescription),
        Prompt = nameof(L10n.Service.NamePrompt),
        ResourceType = typeof(L10n.Service))]
    public string Name { get; set; }

    /// <summary>
    /// Notes from the placer.
    /// </summary>
    [Display(GroupName = nameof(L10n.Service.NoteGroupName),
            ShortName = nameof(L10n.Service.NoteShortName),
            Name = nameof(L10n.Service.NoteName),
            Description = nameof(L10n.Service.NoteDescription),
            Prompt = nameof(L10n.Service.NotePrompt),
            ResourceType = typeof(L10n.Service))]
    public Note Note { get; set; }

    /// <summary>
    /// Ordering value, according to the provider's sorting.
    /// </summary>
    [Display(GroupName = nameof(L10n.Service.RankGroupName),
            ShortName = nameof(L10n.Service.RankShortName),
            Name = nameof(L10n.Service.RankName),
            Description = nameof(L10n.Service.RankDescription),
            Prompt = nameof(L10n.Service.RankPrompt),
            ResourceType = typeof(L10n.Service))]
    public int? Rank { get; set; }

    /// <summary>
    /// List of problems reported by the provider.
    /// </summary>
    [Display(GroupName = nameof(L10n.Service.ProblemsGroupName),
            ShortName = nameof(L10n.Service.ProblemsShortName),
            Name = nameof(L10n.Service.ProblemsName),
            Description = nameof(L10n.Service.ProblemsDescription),
            ResourceType = typeof(L10n.Service))]
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
    public override int GetHashCode() => ServiceId?.GetHashCode() ?? 0;

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

