using FluentValidation;
using Skyware.Arenal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Validation
{

    /// <summary>
    /// Validator for <see cref="Person"/>.
    /// </summary>
    public class PersonValidator : AbstractValidator<Person>
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PersonValidator()
        {

            //Names
            RuleFor(x => new { x.GivenName, x.MiddleName, x.FamilyName }).
                Must(x => string.Join(string.Empty, new string[] { x.GivenName ?? string.Empty, x.MiddleName ?? string.Empty, x.FamilyName ?? string.Empty }).Replace(" ", "").Length >=2 ).
                WithMessage($"Total length of the names of the {nameof(Person)} must be at leas two characters.");

            //Date of birth
            When(x => x.DateOfBirth.HasValue, 
                () => RuleFor(x => x.DateOfBirth).
                    Must(d => d.Value <= DateTime.Now).
                    WithMessage($"{nameof(Person.DateOfBirth)} must be before current date."));

        }

    }

}
