using FluentValidation;
using Skyware.Arenal.Model;
using Skyware.Arenal.Validation;
using System.Linq;

namespace Skyware.Arenal.Validation;

/// <summary>
/// Validator for <see cref="PersonBase"/>
/// </summary>
public class PersonBaseValidator : AbstractValidator<PersonBase>
{

    /// <summary>
    /// Default constructor
    /// </summary>
    public PersonBaseValidator()
    {

        //Identifiers (if any)
        RuleForEach(x => x.Identifiers)
            .SetValidator(new IdentifierValidator(), new string[] { nameof(Patient), "default" });
        RuleFor(x => x.Identifiers)
            .Must(i => i is null || i.GroupBy(x => new { x.Authority, x.Dictionary }).Any(z => z.Count() == 1))
            .WithMessage($"{nameof(PersonBase.Identifiers)} must contain unique set of {nameof(Identifier)} (pair {nameof(Identifier.Authority)} and {nameof(Identifier.Dictionary)}).");
        RuleFor(x => x.Identifiers)
            .Must(i => i is null || i.Count() < PersonBase.MAX_IDENTIFIERS)
            .WithMessage($"Number of {nameof(PersonBase.Identifiers)} must be less than {PersonBase.MAX_IDENTIFIERS}.");

        //Full name
        RuleFor(x => x.FullName)
            .Length(PersonBase.NAMES_MIN_LEN, PersonBase.NAMES_MAX_LEN)
            .WithMessage($"Total length of the names of the {nameof(Patient)} must be at least {PersonBase.NAMES_MIN_LEN} characters.");

        //Contacts (if any)
        RuleFor(x => x.Contacts)
            .Must(i => i is null || i.Count() < PersonBase.MAX_CONTACTS)
            .WithMessage($"Number of {nameof(PersonBase.Contacts)} must be less than {PersonBase.MAX_CONTACTS}.");

    }

}
