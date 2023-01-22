namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Service (examination).
    /// </summary>
    public class Service
    {

        /// <summary>
        /// Coding system for the service identifier such as 'org.loinc', etc.
        /// </summary>
        public string CodingSystem { get; set; }

        /// <summary>
        /// Code of the service such as '57021-8' (CBC in LOINC).
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Name of the service (optional).
        /// </summary>
        public string Name { get; set; }

    }

}

