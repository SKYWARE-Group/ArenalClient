using FluentValidation;
using Skyware.Arenal.Model;
using Skyware.Arenal.Validation;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

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
            .WithMessage($"The number of {nameof(PersonBase.Identifiers)} must be less than {PersonBase.MAX_IDENTIFIERS}.");

        //Full name
        RuleFor(x => x.FullName)
            .Length(PersonBase.NAMES_MIN_LEN, PersonBase.NAMES_MAX_LEN)
            .WithMessage($"Total length of the names of the {nameof(Patient)} must be between {PersonBase.NAMES_MIN_LEN} and {PersonBase.NAMES_MAX_LEN} characters.");

        //Contacts (if any)
        RuleFor(x => x.Contacts)
            .Must(i => i is null || i.Count() < PersonBase.MAX_CONTACTS)
            .WithMessage($"The number of {nameof(PersonBase.Contacts)} must be less than {PersonBase.MAX_CONTACTS}.");

        //Contacts diff reference and same content
        RuleFor(x => x.Contacts)
            .Custom((p, context) =>
            {
                if (p is null)
                    return;

                foreach (Contact contact in p)
                    if (p.Any(cint => cint != contact && cint.Equals(contact)))
                        context.AddFailure($"{nameof(PersonBase.Contacts)} can't have duplicates.");
            });

    }

}
