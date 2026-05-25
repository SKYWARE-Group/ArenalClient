using FluentValidation;
using Skyware.Arenal.ApiModel.Model;
using System;

namespace Skyware.Arenal.ApiModel.Validation;


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

        Include(new PersonBaseValidator());

        //Date of birth (if set)
        When(x => x.DateOfBirth.HasValue, () => RuleFor(x => x.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(Patient.MIN_DOB)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage($"{nameof(Patient.DateOfBirth)} must be before current date (UTC)."));

    }

}
