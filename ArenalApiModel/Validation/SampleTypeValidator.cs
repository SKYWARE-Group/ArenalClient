using FluentValidation;
using Skyware.Arenal.Model;

namespace Skyware.Arenal.Validation
{

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

            //AditiveId
            When(x => x.AditiveId != null, () =>
                RuleFor(x => x.AditiveId)
                .SetValidator(new IdentifierValidator(), new string[] { nameof(SampleType), "default" })
            );

            //AlternativeIdentifiers
            When(x => x.AlternativeIdentifiers != null, () =>
                RuleForEach(x => x.AlternativeIdentifiers)
                .SetValidator(new IdentifierValidator(), new string[] { nameof(SampleType), "default" })
            );

        }

    }

}
