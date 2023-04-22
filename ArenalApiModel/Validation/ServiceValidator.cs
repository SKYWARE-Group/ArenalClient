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

        //ServiceId
        RuleFor(x => x.ServiceId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"The property {nameof(Service.ServiceId)} of a {nameof(Service)} is mandatory.")
            .SetValidator(new IdentifierValidator(), new string[] { nameof(Service), "default" });

    }

    /// <summary>
    /// Validator depending on order status
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
