using FluentValidation;
using Skyware.Arenal.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Skyware.Arenal.Validation;


/// <summary>
/// Validator for <see cref="Order"/>.
/// </summary>
public class OrderValidator : AbstractValidator<Order>
{

    /// <summary>
    /// All workflows, where samples must be provided
    /// </summary>
    private static readonly string[] WORKFLOWS_W_SAMPLES = new[] { Workflows.LAB_SCO, Workflows.LAB_MCP };

    /// <summary>
    /// All workflows, where provider is mandatory
    /// </summary>
    private static readonly string[] WORKFLOWS_W_PROVIDERS = new[] { Workflows.LAB_SCO };

    /// <summary>
    /// Default constructor.
    /// </summary>
    public OrderValidator()
    {

        //Workflow
        RuleFor(x => x.Workflow)
            .Must(x => Helpers.GetAllStringConstants(typeof(Workflows))
            .Any(c => c.Equals(x)))
            .WithMessage($"The property {nameof(Order.Workflow)} must be among values defined in {nameof(Workflows)}.");

        //PlacerId
        RuleFor(x => x.PlacerId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage($"The property {nameof(Order.PlacerId)} is mandatory.");

        //ProviderId (conditional)
        When(x => !string.IsNullOrWhiteSpace(x.Workflow) && WORKFLOWS_W_PROVIDERS.Any(w => w.Equals(x.Workflow, System.StringComparison.InvariantCultureIgnoreCase)), () =>
        {
            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .Must(o => !string.IsNullOrWhiteSpace(o.ProviderId))
                .WithName(x => nameof(x.ProviderId))
                .WithMessage(z => $"In workflow '{z.Workflow}' {nameof(Order)} must have non-null and non-empty value for {nameof(Order.ProviderId)}.")
                .Must(o => !o.ProviderId.Equals(o.PlacerId, System.StringComparison.InvariantCultureIgnoreCase))
                .WithName(x => nameof(x.ProviderId))
                .WithMessage(z => $"In workflow '{z.Workflow}' {nameof(Order)} {nameof(Order.ProviderId)} and {nameof(Order.PlacerId)} can't be equal.");
        });

        //Provider's fields when placing order
        When(x => x.Status == OrderStatuses.AVAILABLE, () =>
        {
            RuleFor(x => x.ProviderNote)
                .Null()
                .WithMessage($"When {nameof(Order.Status)} is '{OrderStatuses.AVAILABLE}', {nameof(Order.ProviderNote)} must be null.");
        });

        //Patient (required)
        RuleFor(x => x.Patient)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"{nameof(Order)} must have a non-null {nameof(Order.Patient)}.")
            .SetValidator(new PatientValidator());

        //Services (required and valid)
        RuleFor(x => x.Services)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage($"{nameof(Order)} must have at least one {nameof(Service)}.")
            .Must(s => s.GroupBy(x => x.ServiceId).Any(z => z.Count() == 1))
            .WithMessage($"{nameof(Order)} must contain unique set of {nameof(Order.Services)}.");
        RuleForEach(x => x.Services)
            .SetValidator(z => new ServiceValidator(z.Status));

        //Samples (conditional)
        When(o => !string.IsNullOrWhiteSpace(o.Workflow) && WORKFLOWS_W_SAMPLES.Any(w => w.Equals(o.Workflow, System.StringComparison.InvariantCultureIgnoreCase)), () =>
        {
            RuleFor(ord => ord.Samples)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(x => $"In workflow '{x.Workflow}' {nameof(Order)} must have at least one {nameof(Sample)}.");
        });
        RuleForEach(x => x.Samples)
            .SetValidator(z => new SampleValidator(z.Status));
        When(x => x.Samples is not null, () =>
        {
            RuleFor(x => x.Samples)
            .Must(s => s.GroupBy(z => z.SampleId).Any(c => c.Count() == 1))
            .WithMessage($"{nameof(Order)} must contain unique set of {nameof(Order.Services)}.");
        });
    }

}
