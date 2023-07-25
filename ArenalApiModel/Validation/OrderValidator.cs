using FluentValidation;
using Skyware.Arenal.Model;
using System;
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
    private static readonly string[] WORKFLOWS_W_SAMPLES = new[] { Workflows.LAB_SCO, Workflows.LAB_MCP };

    /// <summary>
    /// All workflows, where provider is mandatory
    /// </summary>
    private static readonly string[] WORKFLOWS_W_PROVIDERS = new[] { Workflows.LAB_SCO };

    /// <summary>
    /// Orders that has no provider party
    /// </summary>
    private static readonly string[] WORKFLOWS_SELF = new[] { Workflows.LAB_MCP, Workflows.LAB_PSO };

    /// <summary>
    /// Workflows where end user price is mandatory
    /// </summary>
    private static readonly string[] WORKFLOWS_W_PRICES = new[] { Workflows.LAB_PSO };

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
        When(x => !string.IsNullOrWhiteSpace(x.Workflow) && WORKFLOWS_SELF.Any(w => w.Equals(x.Workflow, System.StringComparison.InvariantCultureIgnoreCase)), () =>
        {
            RuleFor(x => x.ProviderId)
                .Empty()
                .WithMessage(z => $"In workflow '{z.Workflow}' {nameof(Order)} must have null or empty value for {nameof(Order.ProviderId)}.");
        });

        // PlacerOrderId
        RuleFor(x => x.PlacerOrderId)
            .MaximumLength(Order.MAX_ORDER_LOCAL_ID)
            .WithMessage($"The length of the {nameof(Order.PlacerOrderId)} must be {Order.MAX_ORDER_LOCAL_ID} or less characters.");

        // ProviderOrderId
        RuleFor(x => x.ProviderOrderId)
            .Null()
                .When(x => x.Status == OrderStatuses.AVAILABLE)
                .WithMessage($"{nameof(Order.ProviderOrderId)} must be null when {nameof(Order.Status)} is equals to '{OrderStatuses.AVAILABLE}'.")
            .MaximumLength(Order.MAX_ORDER_LOCAL_ID)
                .When(x => x.Status != OrderStatuses.AVAILABLE)
                .WithMessage($"The length of the {nameof(Order.ProviderOrderId)} must be {Order.MAX_ORDER_LOCAL_ID} or less characters.");


        // PlacerNote (if presents)
        RuleFor(x => x.PlacerNote)
            .SetValidator(new NoteValidator())
            .When(x => x.PlacerNote is not null);

        //Doctor -----------

        // ProviderNote
        When(x => x.Status == OrderStatuses.AVAILABLE, () =>
        {
            // When placing order
            RuleFor(x => x.ProviderNote)
                .Null()
                .WithMessage($"When {nameof(Order.Status)} is '{OrderStatuses.AVAILABLE}', {nameof(Order.ProviderNote)} must be null.");
        }).Otherwise(() =>
        {
            // When taking/rejecting
            RuleFor(x => x.ProviderNote)
                .SetValidator(new NoteValidator())
                .When(x => x.ProviderNote is not null);
        });

        // Patient (required)
        RuleFor(x => x.Patient)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"{nameof(Order)} must have a non-null {nameof(Order.Patient)}.")
            .SetValidator(new PatientValidator());

        // Expiration
        When(x => x.Workflow == Workflows.LAB_PSO, () =>
            { 
                RuleFor(x => x.Expiration)
                .Must(x => x.HasValue)
                .WithMessage($"In workflow '{Workflows.LAB_PSO}', field '{nameof(Order.Expiration)}' is mandatory."); 
            });
        When(x => x.Expiration.HasValue, () =>
            {
                RuleFor(x => x.Expiration)
                    .Must(x => x.Value > DateTime.UtcNow && x.Value < DateTime.UtcNow.AddDays(Order.MAX_EXPIRATION_DAYS));
            });


        // LinkedReferrals
        RuleFor(x => x.LinkedReferrals)
            .Cascade(CascadeMode.Stop)
            .Must(x => x.Count() <= Order.MAX_LINKED_REFERRALS)
                .When(x => x.LinkedReferrals is not null)
                .WithMessage($"{nameof(Order.LinkedReferrals)} must contains maximum {Order.MAX_LINKED_REFERRALS} items.");
        //TODO: Implement LinkedReferral validator 

        // Services (required and valid)
        RuleFor(x => x.Services)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage($"{nameof(Order)} must have at least one {nameof(Service)}.")
            .Must(s => s.GroupBy(x => x.ServiceId).Any(z => z.Count() == 1))
            .WithMessage($"{nameof(Order)} must contain unique set of {nameof(Order.Services)}.");
        RuleForEach(x => x.Services)
            .SetValidator(z => new ServiceValidator(z.Status));
        When(o => !string.IsNullOrWhiteSpace(o.Workflow) && WORKFLOWS_W_PRICES.Any(w => w.Equals(o.Workflow, StringComparison.InvariantCultureIgnoreCase)), () =>
        {
            RuleFor(ord => ord.Services)
                .Cascade(CascadeMode.Stop)
                .Must(x => x.Any(s => s.EndUserPrice.HasValue))
                .WithMessage(x => $"In workflow '{x.Workflow}' all {nameof(Order.Services)} must have a non-null {nameof(Service.EndUserPrice)}.");
        });

        // Samples (conditional)
        When(o => !string.IsNullOrWhiteSpace(o.Workflow) && WORKFLOWS_W_SAMPLES.Any(w => w.Equals(o.Workflow, StringComparison.InvariantCultureIgnoreCase)), () =>
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
                .WithMessage($"{nameof(Order)} must contain unique set of {nameof(Order.Samples)}.");
        });
    }

}
