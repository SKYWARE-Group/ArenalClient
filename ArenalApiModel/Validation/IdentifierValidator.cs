using FluentValidation;
using Skyware.Arenal.Model;
using System.Linq;

namespace Skyware.Arenal.Validation;


/// <summary>
/// <see cref="Identifier"/> validator.
/// </summary>
public class IdentifierValidator : AbstractValidator<Identifier>
{

    /// <summary>
    /// Default constructor.
    ///// </summary>
    public IdentifierValidator()
    {


        //When Patient
        RuleSet(nameof(Patient), () =>
        {
            RuleFor(x => x.Authority)
                .Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Patient)).Any(a => a == x))
                .WithMessage(x => $"Value '{x.Authority}' for {nameof(Identifier.Authority)} is invalid for object of type {nameof(Patient)}.");
        });

        //When Doctor
        RuleSet(nameof(Doctor), () =>
        {
            RuleFor(x => x.Authority)
                .Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Doctor)).Any(a => a == x))
                .WithMessage(x => $"Value '{x.Authority}' for {nameof(Identifier.Authority)} is invalid for object of type {nameof(Doctor)}.");
        });

        //When Service
        RuleSet(nameof(Service), () =>
        {
            RuleFor(x => x.Authority)
                .Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Service)).Any(a => a == x))
                .WithMessage(x => $"Value '{x.Authority}' for {nameof(Identifier.Authority)} is invalid for object of type {nameof(Service)}.");
        });

        //When Sample
        RuleSet(nameof(Sample), () =>
        {
            RuleFor(x => x.Authority)
                .Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Sample)).Any(a => a == x))
                .WithMessage(x => $"Value '{x.Authority}' for {nameof(Identifier.Authority)} is invalid for object of type {nameof(Sample)}.");
        });

        //When SampleType
        RuleSet(nameof(SampleType), () =>
        {
            RuleFor(x => x.Authority)
                .Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(SampleType)).Any(a => a == x))
                .WithMessage(x => $"Value '{x.Authority}' for {nameof(Identifier.Authority)} is invalid for object of type {nameof(SampleType)}.");
        });

        //All (default)
        RuleFor(x => x.Value)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Identifier.VALUE_MAX_LEN)
            .WithMessage($"When {nameof(Identifier)} is set, its property {nameof(Identifier.Value)} must have a non-null, non-empty value, up to {Identifier.VALUE_MAX_LEN} characters.");
        RuleFor(x => x.Authority)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Identifier.AUTHORITY_MAX_LEN)
            .WithMessage($"When {nameof(Identifier)} is set, its property {nameof(Identifier.Authority)} must have a non-null, non-empty value, up to {Identifier.AUTHORITY_MAX_LEN} characters.");

        //Dictionary
        //Currently check only against predefined values therefore no length is checked
        When(i => !string.IsNullOrWhiteSpace(i.Dictionary), () =>
        {
            RuleFor(id => id)
                .Cascade(CascadeMode.Stop)
                .Must(x => Helpers.GetStringConstants(typeof(Dictionaries), x.Authority).Any(dict => x.Dictionary == dict))
                .WithName(nameof(Identifier.Dictionary))
                .WithMessage(x => $"The value '{x.Dictionary}' isn't allowed for {nameof(Identifier.Authority)} '{x.Authority}'.");

        });

    }

}
