﻿using FluentValidation;
using Skyware.Arenal.Model;
using System.Linq;

namespace Skyware.Arenal.Validation
{

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

            RuleSet(nameof(Patient), () =>
            {
                RuleFor(x => x.Authority).Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Patient)).Any(a => a == x));
            });

            RuleSet(nameof(Doctor), () =>
            {
                RuleFor(x => x.Authority).Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Doctor)).Any(a => a == x));
            });

            RuleSet(nameof(Service), () =>
            {
                RuleFor(x => x.Authority).Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Service)).Any(a => a == x));
            });

            RuleSet(nameof(Sample), () =>
            {
                RuleFor(x => x.Authority).Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(Sample)).Any(a => a == x));
            });

            RuleSet(nameof(SampleType), () =>
            {
                RuleFor(x => x.Authority).Must(x => Helpers.GetStringConstants(typeof(Authorities), typeof(SampleType)).Any(a => a == x));
            });

            RuleFor(x => x.Value).NotEmpty().WithMessage($"When {nameof(Identifier)} is set, its property {nameof(Identifier.Value)} mustn't be null or empty.");
        }

    }

}