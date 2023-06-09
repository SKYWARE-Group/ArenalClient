using FluentValidation;
using Skyware.Arenal.Model;

namespace Skyware.Arenal.Validation;


/// <summary>
/// Validator for <see cref="SampleType"/>.
/// </summary>
public class SampleTypeValidator : AbstractValidator<SampleType>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public SampleTypeValidator()
    {

        //TypeId
        RuleFor(x => x.TypeId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"The property {nameof(SampleType.TypeId)} of a {nameof(SampleType)} is mandatory.")
            .SetValidator(new IdentifierValidator(), new string[] { nameof(SampleType), "default" });

        //AdditiveId
        When(x => x.AdditiveId != null, () =>
            RuleFor(x => x.AdditiveId)
                .SetValidator(new IdentifierValidator(), new string[] { nameof(SampleType), "default" })
        );

        //AdditiveId
        When(x => x.BodyPartId != null, () =>
            RuleFor(x => x.BodyPartId)
                .SetValidator(new IdentifierValidator(), new string[] { nameof(SampleType), "default" })
        );

        //AlternateIdentifiers
        When(x => x.AlternateIdentifiers != null, () =>
            RuleForEach(x => x.AlternateIdentifiers)
                .SetValidator(new IdentifierValidator(), new string[] { nameof(SampleType), "default" }
            )
        );

    }

}
