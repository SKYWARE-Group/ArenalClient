using FluentValidation;
using Skyware.Arenal.Model;
using System.Linq;

namespace Skyware.Arenal.Validation
{

    /// <summary>
    /// Validator for <see cref="Order"/>.
    /// </summary>
    public class OrderValidator : AbstractValidator<Order>
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OrderValidator()
        {

            //Workflow
            RuleFor(x => x.Workflow).
                Must(x => Helpers.GetAllStringConstants(typeof(Workflows)).Any(c => c.Equals(x))).
                WithMessage($"The property {nameof(Order.Workflow)} must be among values defined in {nameof(Workflows)}.");

            //ProviderId
            When(x => !string.IsNullOrWhiteSpace(x.Workflow) && x.Workflow.Equals(Workflows.LAB_SCO), () =>
            {
                RuleFor(x => x.ProviderId).NotEmpty().WithMessage($"In workflow '{Workflows.LAB_SCO}' {nameof(Order)} must have {nameof(Order.ProviderId)}.");
            });

            //Patient
            RuleFor(x => x.Patient).
                Cascade(CascadeMode.Stop).
                NotNull().
                WithMessage($"{nameof(Order)} must have a {nameof(Order.Patient)}.").
                SetValidator(new PatientValidator());

            //Services
            RuleFor(x => x.Services).
                NotEmpty().
                WithMessage($"{nameof(Order)} must have at least one {nameof(Service)}.");
            RuleForEach(x => x.Services).SetValidator(new ServiceValidator());

            //Samples
            When(x => !string.IsNullOrWhiteSpace(x.Workflow) && x.Workflow.Equals(Workflows.LAB_SCO), () =>
            {
                RuleFor(x => x.Samples).NotEmpty().WithMessage($"In workflow '{Workflows.LAB_SCO}' {nameof(Order)} must have at least one {nameof(Sample)}.");
            });

        }

    }
}
