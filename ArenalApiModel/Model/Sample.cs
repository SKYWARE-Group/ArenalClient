using System;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Sample (material) to be tested.
    /// </summary>
    public class Sample
    {

        /// <summary>
        /// Identifier of the sample type.
        /// </summary>
        public Identifier TypeId { get; set; }

        /// <summary>
        /// Identifier of the sample.
        /// </summary>
        public Identifier Id { get; set; }

        /// <summary>
        /// Date and time the sample has been taken.
        /// Optional.
        /// </summary>
        public DateTime? Taken { get; set; }

    }

}

