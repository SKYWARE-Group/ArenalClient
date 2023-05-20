using System.ComponentModel.DataAnnotations;

namespace Skyware.Arenal.Model
{

    /// <summary>.
    /// Represents different type of problems and discrepancies.
    /// </summary>
    public class Problem
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public Problem() { }

        /// <summary>
        /// Instantiates a <see cref="Problem"/> wit Id and message.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        public Problem(Identifier identifier, string message = null) : this ()
        { 
            Identifier = identifier;
            if (!string.IsNullOrEmpty(message))
            {
                Note = new Note(message);
            }
        }

        /// <summary>
        /// Identifier of a problem.
        /// </summary>
        [Display(GroupName = nameof(L10n.Problem.Problem.IdentifiersGroupName),
            ShortName = nameof(L10n.Problem.Problem.IdentifiersShortName),
            Name = nameof(L10n.Problem.Problem.IdentifiersName),
            Description = nameof(L10n.Problem.Problem.IdentifiersDescription),
            Prompt = nameof(L10n.Problem.Problem.IdentifiersPrompt),
            ResourceType = typeof(L10n.Problem.Problem))]
        public Identifier Identifier { get; set; }

        /// <summary>
        /// Human readable description of a problem.
        /// </summary>
        [Display(GroupName = nameof(L10n.Problem.Problem.NoteGroupName),
            ShortName = nameof(L10n.Problem.Problem.NoteShortName),
            Name = nameof(L10n.Problem.Problem.NoteName),
            Description = nameof(L10n.Problem.Problem.NoteDescription),
            Prompt = nameof(L10n.Problem.Problem.NotePrompt),
            ResourceType = typeof(L10n.Problem.Problem))]
        public Note Note { get; set; }

        /// <summary>
        /// Severity of the problem.
        /// </summary>
        [Display(GroupName = nameof(L10n.Problem.Problem.SeverityGroupName),
            ShortName = nameof(L10n.Problem.Problem.SeverityShortName),
            Name = nameof(L10n.Problem.Problem.SeverityName),
            Description = nameof(L10n.Problem.Problem.SeverityDescription),
            Prompt = nameof(L10n.Problem.Problem.SeverityPrompt),
            ResourceType = typeof(L10n.Problem.Problem))]
        public Severity Severity { get; set; } = Severity.Error;

    }

}
