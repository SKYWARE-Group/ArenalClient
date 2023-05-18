using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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
        [Display(GroupName = nameof(L10n.ServiceProblem.ServiceIdGroup),
            ShortName = nameof(L10n.ServiceProblem.ServiceIdShortName),
            Name = nameof(L10n.ServiceProblem.ServiceIdName),
            Description = nameof(L10n.ServiceProblem.ServiceIdDescription),
            ResourceType = typeof(L10n.ServiceProblem))]
        public string ServiceId { get; set; }

        /// <summary>
        /// The problem object
        /// </summary>
        [Display(GroupName = nameof(L10n.ServiceProblem.ProblemGroup),
            ShortName = nameof(L10n.ServiceProblem.ProblemShortName),
            Name = nameof(L10n.ServiceProblem.ProblemName),
            Description = nameof(L10n.ServiceProblem.ProblemDescription),
            ResourceType = typeof(L10n.ServiceProblem))]
        public Problem Problem { get; set; }

    }

}
