using FluentValidation;
using Skyware.Arenal.Model;
using System.Linq;

namespace Skyware.Arenal.Validation;


/// <summary>
/// Validator for <see cref="Order"/>.
/// </summary>
public class OrderValidator : AbstractValidator<Order>
{

    /// <summary>
    /// All workflows, where samples must be provided
    /// </summary>
    public static readonly string[] WORKFLOWS_W_SAMPLES = new[] {Workflows.LAB_SCO };

    /// <summary>
    /// All workflows, where provider is mandatory
    /// </summary>
    public static readonly string[] WORKFLOWS_W_PROVIDERS = new[] {Workflows.LAB_SCO }; 

    /// <summary>
    /// Default constructor.
    /// </summary>
    public OrderValidator()
    {

        //Workflow
        RuleFor(x => x.Workflow).
            Must(x => Helpers.GetAllStringConstants(typeof(Workflows)).Any(c => c.Equals(x))).
            WithMessage($"The property {nameof(Order.Workflow)} must be among values defined in {nameof(Workflows)}.");

        //ProviderId (conditional)
        When(x => !string.IsNullOrWhiteSpace(x.Workflow) && WORKFLOWS_W_PROVIDERS.Any(w => w.Equals(x.Workflow, System.StringComparison.InvariantCultureIgnoreCase)), () =>
        {
            RuleFor(x => x.ProviderId).NotEmpty().WithMessage(z => $"In workflow '{z.Workflow}' {nameof(Order)} must have {nameof(Order.ProviderId)}.");
        });

        //Patient (required)
        RuleFor(x => x.Patient).
            Cascade(CascadeMode.Stop).
            NotNull().
            WithMessage($"{nameof(Order)} must have a {nameof(Order.Patient)}.").
            SetValidator(new PatientValidator());

        //Services (required and valid)
        RuleFor(x => x.Services).
            NotEmpty().
            WithMessage($"{nameof(Order)} must have at least one {nameof(Service)}.");
        RuleForEach(x => x.Services).SetValidator(new ServiceValidator());

        //Samples (conditional)
        When(x => !string.IsNullOrWhiteSpace(x.Workflow) && WORKFLOWS_W_SAMPLES.Any(w => w.Equals(x.Workflow, System.StringComparison.InvariantCultureIgnoreCase)), () =>
        {
            RuleFor(x => x.Samples).NotEmpty().WithMessage(z => $"In workflow '{z.Workflow}' {nameof(Order)} must have at least one {nameof(Sample)}.");
        });

    }

}
