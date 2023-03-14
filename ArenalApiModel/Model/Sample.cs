﻿using System;

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
        /// Identifier of additive.
        /// </summary>
        public Identifier AditiveId { get; set; }

        /// <summary>
        /// Identifier of the sample.
        /// </summary>
        public Identifier Id { get; set; }

        /// <summary>
        /// Date and time the sample has been taken.
        /// Optional.
        /// </summary>
        public DateTime? Taken { get; set; }

        /// <summary>
        /// Notes to the sample.
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Sample() { }

        /// <summary>
        /// Instantiates a Sample with strictly HL7 coded sample type and locally generated barcode.
        /// </summary>
        /// <param name="sampleType"></param>
        /// <param name="sampleAdditive"></param>
        /// <param name="barcode"></param>
        /// <param name="taken"></param>
        /// <param name="note"></param>
        public Sample(string sampleType, string sampleAdditive, string barcode, DateTime? taken = null, string note = null) : this()
        {
            if (!string.IsNullOrWhiteSpace(sampleType)) TypeId = new Identifier(Authorities.HL7, Dictionaries.HL7_0487_SampleType, sampleType);
            if (!string.IsNullOrWhiteSpace(sampleAdditive)) AditiveId = new Identifier(Authorities.HL7, Dictionaries.HL7_0487_SampleAdditive, sampleAdditive);
            Taken = taken;
            if (!string.IsNullOrWhiteSpace(barcode)) Id = new Identifier(Authorities.LOCAL, null, barcode);
            if (!string.IsNullOrWhiteSpace(note)) Note = new Note(note);
        }

    }

}

