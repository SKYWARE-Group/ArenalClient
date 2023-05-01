using FluentValidation;
using Skyware.Arenal.Model;
using System;
using System.Linq;

namespace Skyware.Arenal.Validation;


/// <summary>
/// Validator for <see cref="Patient"/>.
/// </summary>
public class PatientValidator : AbstractValidator<Patient>
{

    private const int NAMES_MIN_LEN = 2;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public PatientValidator()
    {

        //Identifiers (if any)
        When(x => x.Identifiers is not null, () =>
        {
            RuleForEach(x => x.Identifiers)
                .SetValidator(new IdentifierValidator(), new string[] { nameof(Patient), "default" });
            RuleFor(x => x.Identifiers)
                .Must(i => i.GroupBy(x => new { x.Authority, x.Dictionary }).Any(z => z.Count() == 1))
                .WithMessage($"{nameof(Patient.Identifiers)} must contain unique set of {nameof(Identifier)}.");
        });

        //Names
        RuleFor(x => new { x.GivenName, x.MiddleName, x.FamilyName })
            .Must(x =>
                string.Join(string.Empty, new string[] { x.GivenName.EmptyIfNull(), x.MiddleName.EmptyIfNull(), x.FamilyName.EmptyIfNull() })
                    .Replace(" ", "")
                    .Length >= NAMES_MIN_LEN)
            .WithMessage($"Total length of the names of the {nameof(Patient)} must be at least {NAMES_MIN_LEN} characters.");

        //Date of birth (if set)
        When(x => x.DateOfBirth.HasValue, () => RuleFor(x => x.DateOfBirth)
            .Must(d => d.Value <= DateTime.UtcNow)
            .WithMessage($"{nameof(Patient.DateOfBirth)} must be before current date (UTC)."));

    }

}
