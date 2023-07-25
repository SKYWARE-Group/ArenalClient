using FluentValidation;
using Skyware.Arenal.Model;
using System;
using System.Linq;

namespace Skyware.Arenal.Validation;


/// <summary>
/// Validator for <see cref="Sample"/>.
/// </summary>
public class SampleValidator : AbstractValidator<Sample>
{

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SampleValidator()
    {

        //SampleType
        RuleFor(x => x.SampleType)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"The property {nameof(Sample.SampleType)} of a {nameof(Sample)} is mandatory.")
            .SetValidator(new SampleTypeValidator());

        //ServiceId
        RuleFor(x => x.SampleId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"The property {nameof(Sample.SampleId)} of a {nameof(Sample)} is mandatory.")
            .SetValidator(new IdentifierValidator(), new string[] { nameof(Sample), "default" });

        //Taken (if set)
        When(x => x.Taken.HasValue, () => RuleFor(x => x.Taken)
            .Must(d => d.Value <= DateTime.UtcNow)
            .WithMessage($"{nameof(Sample.Taken)} must be before current date.")
        );

    }

    /// <summary>
    /// Validator depending on order status
    /// </summary>
    /// <param name="orderStatus"></param>
    public SampleValidator(string orderStatus) : this()
    {
        //When placed
        if (orderStatus == OrderStatuses.AVAILABLE)
            RuleFor(x => x.Problems)
                .Null()
                .WithMessage($"When status of {nameof(Order)} is '{OrderStatuses.AVAILABLE}', {nameof(Sample.Problems)} of {nameof(Sample)} must be null.");
    }

}
