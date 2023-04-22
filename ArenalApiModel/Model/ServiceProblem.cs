namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represent a problem or discrepancy, connected with a sample.
    /// </summary>
    public class ServiceProblem
    {

        /// <summary>
        /// Reference to a <see cref="Identifier"/> of a <see cref="Service"/>
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// The problem object
        /// </summary>
        public Problem Problem { get; set; }

    }

}
