using FluentValidation;
using Skyware.Arenal.Model;
using System;

namespace Skyware.Arenal.Validation
{

    /// <summary>
    /// Validator for <see cref="Patient"/>.
    /// </summary>
    public class PatientValidator : AbstractValidator<Patient>
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PatientValidator()
        {

            //Identifiers (if any)
            When(x => x.Identifiers != null, () =>
            {
                RuleForEach(x => x.Identifiers).SetValidator(new IdentifierValidator(), new string[] { nameof(Patient), "default" });
            });

            //Names
            RuleFor(x => new { x.GivenName, x.MiddleName, x.FamilyName }).
                Must(x => string.Join(string.Empty, new string[] { x.GivenName ?? string.Empty, x.MiddleName ?? string.Empty, x.FamilyName ?? string.Empty }).Replace(" ", "").Length >= 2).
                WithMessage($"Total length of the names of the {nameof(Patient)} must be at leas two characters.");

            //Date of birth (if set)
            When(x => x.DateOfBirth.HasValue,
                () => RuleFor(x => x.DateOfBirth).
                    Must(d => d.Value <= DateTime.Now).
                    WithMessage($"{nameof(Patient.DateOfBirth)} must be before current date."));

            

        }

    }

}
