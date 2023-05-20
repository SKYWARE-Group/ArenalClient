using Skyware.Arenal.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Skyware.Arenal.Model;


/// <summary>
/// Sample type (laboratory)
/// </summary>
public class SampleType
{

    private static SampleTypeValidator _validator;

    /// <summary>
    /// Identifier of the sample type.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleType.SampleType.TypeIdShortName),
        Name = nameof(L10n.SampleType.SampleType.TypeIdName),
        Description = nameof(L10n.SampleType.SampleType.TypeIdDescription),
        Prompt = nameof(L10n.SampleType.SampleType.TypeIdPrompt),
        ResourceType = typeof(L10n.SampleType.SampleType))]
    public Identifier TypeId { get; set; }

    /// <summary>
    /// Identifier of additive.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleType.SampleType.AdditiveIdShortName),
        Name = nameof(L10n.SampleType.SampleType.AdditiveIdName),
        Description = nameof(L10n.SampleType.SampleType.AdditiveIdDescription),
        ResourceType = typeof(L10n.SampleType.SampleType))]
    public Identifier AdditiveId { get; set; }

    /// <summary>
    /// Additional identifiers, not defined in <see cref="TypeId"/> and <see cref="AdditiveId"/>.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleType.SampleType.AlternateIdentifiersShortName),
        Name = nameof(L10n.SampleType.SampleType.AlternateIdentifiersName),
        Description = nameof(L10n.SampleType.SampleType.AlternateIdentifiersDescription),
        Prompt = nameof(L10n.SampleType.SampleType.AlternateIdentifiersPrompt),
        ResourceType = typeof(L10n.SampleType.SampleType))]
    public IEnumerable<Identifier> AlternateIdentifiers { get; set; }

    /// <summary>
    /// Name of the sample type as it is known by the placer or provider.
    /// </summary>
    [Display(ShortName = nameof(L10n.SampleType.SampleType.NameShortName),
        Name = nameof(L10n.SampleType.SampleType.NameName),
        Description = nameof(L10n.SampleType.SampleType.NameDescription),
        Prompt = nameof(L10n.SampleType.SampleType.NamePrompt),
        ResourceType = typeof(L10n.SampleType.SampleType))]
    public string Name { get; set; }

    public ValidationResult Validate()
    {
        return (_validator ??= new SampleTypeValidator()).Validate(this);
    }

}
