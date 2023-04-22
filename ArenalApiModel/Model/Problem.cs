namespace Skyware.Arenal.Model
{

    /// <summary>.
    /// Represents different type of problems and discrepancies.
    /// </summary>
    public class Problem
    {

        /// <summary>
        /// Identifier of a problem.
        /// </summary>
        public Identifier Identifier { get; set; }

        /// <summary>
        /// Human readable description of a problem.
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Severity of the problem.
        /// </summary>
        public Severity Severity { get; set; } = Severity.Error;

    }

}
