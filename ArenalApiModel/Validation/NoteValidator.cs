using FluentValidation;
using Skyware.Arenal.Model;
using System.Linq;

namespace Skyware.Arenal.Validation;

/// <summary>
/// Validator for a <see cref="Note"/>.
/// </summary>
public class NoteValidator : AbstractValidator<Note>
{

    /// <summary>
    /// Default constructor.
    /// </summary>
    public NoteValidator()
    {

        RuleFor(x => x.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(x => Helpers.GetAllStringConstants(typeof(NoteTypes)).Any(nt => nt.Equals(x, System.StringComparison.InvariantCultureIgnoreCase)))
            .WithMessage(x => $"Value '{x.Type}' for {nameof(Note.Type)} is invalid .");

        RuleFor(x => x.Value)
            .Length(Note.MIN_LEN, Note.MAX_LEN);

    }

}
