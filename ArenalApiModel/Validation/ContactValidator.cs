using FluentValidation;
using Skyware.Arenal.Filters;
using Skyware.Arenal.Model;

namespace Skyware.Arenal.Validation;

/// <summary>
/// Validator for <see cref="Contact"/>
/// </summary>
public class ContactValidator : AbstractValidator<Contact>
{
    /// <summary> Regex for phone Number </summary>
    public const string PHONE_NUMBER_FORMAT = "((\\+|00|)*)([0]*(\\(| |-)*([0-9]{0,3})(\\)| |-)*(-| |)*[0-9]{3})(-| |)*[0-9]{3}(-| |\\(\\))*[0-9]{3}(\\)| |-)*";

    /// <summary>
    /// Default constructor
    /// </summary>
    public ContactValidator()
    {
        // Global rules in all cases
        RuleFor(p => p.Value)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Contact.VALUE_MAX_LEN)
            .WithMessage($"{nameof(Contact)} {nameof(Contact.Value)} cannot be null, empty or longer than {Contact.VALUE_MAX_LEN} characters.");

        // Validation for email contactType
        When(p => p.Type == ContactTypes.EMAIL, () =>
        {
            RuleFor(p => p.Value)
                .EmailAddress()
                .WithMessage($"{nameof(Contact)} {nameof(Contact.Value)} with {nameof(Contact.Type)} {ContactTypes.EMAIL} must be valid email address.");
        });

        // Validation for phone contactType
        //   https://en.wikipedia.org/wiki/List_of_country_calling_codes
        When(p => p.Type == ContactTypes.PHONE, () =>
        {
            RuleFor(p => p.Value)
                .Matches(PHONE_NUMBER_FORMAT)
                .WithMessage($"{nameof(Contact)} {nameof(Contact.Value)} with {nameof(Contact.Type)} {ContactTypes.PHONE} must be valid phone number."); ;
        });
    }
}
