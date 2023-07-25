using FluentValidation;
using Skyware.Arenal.Model;

namespace Skyware.Arenal.Validation;


/// <summary>
/// Validator for <see cref="Service"/>.
/// </summary>
public class ServiceValidator : AbstractValidator<Service>
{

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ServiceValidator()
    {

        // ServiceId
        RuleFor(x => x.ServiceId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"The property {nameof(Service.ServiceId)} of a {nameof(Service)} is mandatory.")
            .SetValidator(new IdentifierValidator(), new string[] { nameof(Service), "default" });

        // Every alternate identifier (if any)
        RuleForEach(x => x.AlternateIdentifiers)
            .SetValidator(new IdentifierValidator(), new string[] { nameof(Service), "default" });

        // Alternate identifiers (if any)
        RuleFor(x => x.AlternateIdentifiers)
            .Must(i => i is null || i.Count < Service.ALTERNATE_IDENTIFIERS_MAX)
            .WithMessage($"The number of {nameof(Service.AlternateIdentifiers)} must be less than {Service.ALTERNATE_IDENTIFIERS_MAX}.");

        // Name
        RuleFor(x => x.Name)
            .MaximumLength(Service.NAME_MAX_LEN);

        // Note (if presents)
        When(x => x.Note is not null, () => {
            RuleFor(x => x.Note)
                .SetValidator(new NoteValidator());
        });

        // EndUserPrice (if presents)
        When(x => x.EndUserPrice is not null, () => {
            RuleFor(x => x.EndUserPrice)
                .Must(x => x >= 0)
                .WithMessage($"The {nameof(Service.EndUserPrice)} must be equal or greater than zero.");
        });

    }

    /// <summary>
    /// Validator depending on order status.
    /// </summary>
    /// <param name="orderStatus"></param>
    public ServiceValidator(string orderStatus) : this()
    {
        //When placed
        if (orderStatus == OrderStatuses.AVAILABLE)
            RuleFor(x => x.Problems)
                .Null()
                .WithMessage($"When status of {nameof(Order)} is '{OrderStatuses.AVAILABLE}', field {nameof(Service.Problems)} of {nameof(Service)} must be null.");
    }

}
