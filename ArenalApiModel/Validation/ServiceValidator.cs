using FluentValidation;
using Skyware.Arenal.Model;

namespace Skyware.Arenal.Validation
{

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

            //Id
            RuleFor(x => x.Id).
                Cascade(CascadeMode.Stop).
                NotNull().
                WithMessage($"The property {nameof(Service.Id)} of a {nameof(Service)} is mandatory.").
                SetValidator(new IdentifierValidator(), new string[] { nameof(Service), "default" });

        }

    }

}
