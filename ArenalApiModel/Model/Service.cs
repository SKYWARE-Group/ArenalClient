namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Service (examination).
    /// </summary>
    public class Service
    {

        /// <summary>
        /// Identifier of a ordered examination or service.
        /// </summary>
        public Identifier Id { get; set; }

        /// <summary>
        /// Name of the service, according to the placer (optional).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Notes from the placer.
        /// </summary>
        public Note Note { get; set; }

    }

}

