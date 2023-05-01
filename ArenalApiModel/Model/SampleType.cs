using FluentValidation.Results;
using Skyware.Arenal.Validation;
using System.Collections.Generic;

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
    public Identifier TypeId { get; set; }

    /// <summary>
    /// Identifier of additive.
    /// </summary>
    public Identifier AdditiveId { get; set; }

    /// <summary>
    /// Additional identifiers, not defined in <see cref="TypeId"/> and <see cref="AdditiveId"/>.
    /// </summary>
    public IEnumerable<Identifier> AlternateIdentifiers { get; set; }

    /// <summary>
    /// Name of the sample type as it is known by the placer or provider.
    /// </summary>
    public string Name { get; set; }

    public ValidationResult Validate()
    {
        return (_validator ??= new SampleTypeValidator()).Validate(this);
    }

}
