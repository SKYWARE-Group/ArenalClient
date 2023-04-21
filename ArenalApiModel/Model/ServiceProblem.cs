namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represent a problem or discrepancy, connected with a sample.
    /// </summary>
    public class ServiceProblem : Problem
    {

        /// <summary>
        /// Reference to a <see cref="Identifier"/> of a <see cref="Service"/>
        /// </summary>
        public string ServiceId { get; set; }

    }

}
