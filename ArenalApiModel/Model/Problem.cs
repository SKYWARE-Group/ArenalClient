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
        public Identifier Identifier { get; set; }

        /// <summary>
        /// Human readable description of a problem.
        /// </summary>
        [Display(GroupName = "Note", ShortName = "Note", Name = "Note",
            Description = $"Description of the problem.",
            Prompt = "Please, enter description note.")]
        public Note Note { get; set; }

        /// <summary>
        /// Severity of the problem.
        /// </summary>
        [Display(GroupName = "Problem", ShortName = "Severity", Name = "Severity",
            Description = $"Specifies the severity of a problem.")]
        public Severity Severity { get; set; } = Severity.Error;

    }

}
