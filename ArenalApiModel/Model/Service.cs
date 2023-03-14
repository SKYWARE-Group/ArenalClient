﻿namespace Skyware.Arenal.Model
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

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Service()
        {

        }

        /// <summary>
        /// Instantiates a Service for laboratory examination, coded with Loinc.
        /// </summary>
        public Service(string loincCode, string name = null, string note = null)
        {
            if (!string.IsNullOrWhiteSpace(loincCode)) Id = new Identifier(Authorities.LOINC, null , loincCode);
            Name = name;
            if (!string.IsNullOrWhiteSpace(note)) Note = new Note(note);
        }


    }

}

