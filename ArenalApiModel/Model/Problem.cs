namespace Skyware.Arenal.Model
{

    /// <summary>.
    /// Represents different type of problems and discrepancies.
    /// </summary>
    public abstract class Problem
    {

        /// <summary>
        /// Identifier of a problem.
        /// </summary>
        public Identifier Identifier { get; set; }

        /// <summary>
        /// Human readable description of a problem.
        /// </summary>
        public Note Note { get; set; }

    }

}
