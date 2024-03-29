﻿using Skyware.Arenal.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Sample (material) to be tested.
    /// </summary>
    public class Sample
    {

        private static SampleValidator _validator;


        /// <summary>
        /// Identifier of the sample type.
        /// </summary>
        [Display(ShortName = nameof(L10n.Sample.Sample.SampleTypeShortName), Name = nameof(L10n.Sample.Sample.SampleTypeName),
            Description = nameof(L10n.Sample.Sample.SampleTypeDescription), Prompt = nameof(L10n.Sample.Sample.SampleTypePrompt),
            ResourceType = typeof(L10n.Sample.Sample))]
        public SampleType SampleType { get; set; }

        /// <summary>
        /// Identifier of the sample (barcode).
        /// </summary>
        [Display(GroupName = nameof(L10n.Sample.Sample.SampleIdGroupName),
            ShortName = nameof(L10n.Sample.Sample.SampleIdShortName),
            Name = nameof(L10n.Sample.Sample.SampleIdName),
            Description = nameof(L10n.Sample.Sample.SampleIdDescription),
            Prompt = nameof(L10n.Sample.Sample.SampleIdPrompt),
            ResourceType = typeof(L10n.Sample.Sample))]
        public Identifier SampleId { get; set; }

        /// <summary>
        /// Date and time the sample has been taken (UTC).
        /// Optional.
        /// </summary>
        [Display(GroupName = nameof(L10n.Sample.Sample.TakenGroupName),
            ShortName = nameof(L10n.Sample.Sample.TakenShortName),
            Name = nameof(L10n.Sample.Sample.TakenName),
            Description = nameof(L10n.Sample.Sample.TakenDescription),
            Prompt = nameof(L10n.Sample.Sample.TakenPrompt),
            ResourceType = typeof(L10n.Sample.Sample))]
        public DateTime? Taken { get; set; }

        /// <summary>
        /// Date and time the order was created (Local date and time).
        /// </summary>
        [Display(GroupName = nameof(L10n.Sample.Sample.LocalTakenGroupName),
            ShortName = nameof(L10n.Sample.Sample.LocalTakenShortName),
            Name = nameof(L10n.Sample.Sample.LocalTakenName),
            Description = nameof(L10n.Sample.Sample.LocalTakenDescription),
            ResourceType = typeof(L10n.Sample.Sample))]
        public DateTime? LocalTaken => Taken?.ToLocalTime();

        /// <summary>
        /// Notes to the sample.
        /// </summary>
        [Display(GroupName = nameof(L10n.Sample.Sample.NoteGroupName),
            ShortName = nameof(L10n.Sample.Sample.NoteShortName),
            Name = nameof(L10n.Sample.Sample.NoteName),
            Description = nameof(L10n.Sample.Sample.NoteDescription),
            Prompt = nameof(L10n.Sample.Sample.NotePrompt),
            ResourceType = typeof(L10n.Sample.Sample))]
        public Note Note { get; set; }

        /// <summary>
        /// List of problems reported by the provider.
        /// Preferred coding system is 'org.hl7' with dictionary '0490'
        /// </summary>
        [Display(GroupName = nameof(L10n.Sample.Sample.ProblemsGroupName),
            ShortName = nameof(L10n.Sample.Sample.ProblemsShortName),
            Name = nameof(L10n.Sample.Sample.ProblemsName),
            Description = nameof(L10n.Sample.Sample.ProblemsDescription),
            ResourceType = typeof(L10n.Sample.Sample))]
        public IList<Problem> Problems { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Sample() { }

        /// <summary>
        /// Instantiates a Sample with strictly HL7 coded sample type and locally generated barcode.
        /// </summary>
        /// <param name="sampleType">HL7 coded sample type, according to HL7 table 0487</param>
        /// <param name="sampleAdditive">HL7 coded sample additive, according to HL7 table 0371</param>
        /// <param name="barcode">Barcode value of the sample</param>
        /// <param name="taken">Date and time when sample is taken</param>
        /// <param name="note">Plain text note</param>
        public Sample(string sampleType, string sampleAdditive, string barcode, DateTime? taken = null, string note = null) : this()
        {
            if (!string.IsNullOrWhiteSpace(sampleType)) SampleType = new SampleType() { TypeId = new Identifier(Authorities.HL7, Dictionaries.HL7_0487_SampleType, sampleType) };
            if (!string.IsNullOrWhiteSpace(sampleAdditive))
            {
                SampleType ??= new SampleType();
                SampleType.AdditiveId = new Identifier(Authorities.HL7, Dictionaries.HL7_0371_SampleAdditive, sampleAdditive);
            }
            Taken = taken;
            if (!string.IsNullOrWhiteSpace(barcode)) SampleId = new Identifier(Authorities.LOCAL, null, barcode);
            if (!string.IsNullOrWhiteSpace(note)) Note = new Note(note);
        }

        /// <summary>
        /// Safely adds a <see cref="Problem"/> to the sample.
        /// </summary>
        /// <param name="problem">A <see cref="Problem"/> to add</param>
        public Sample AddProblem(Problem problem)
        {
            (Problems ??= new List<Problem>()).Add(problem);
            return this;
        }

        /// <summary>
        /// Safely adds a HL7 coded <see cref="Problem"/> to the sample with optional note.
        /// </summary>
        /// <param name="problemCode">HL7 coded problem (table 0490)</param>
        /// <param name="note">A note to the problem</param>
        public Sample AddProblem (string problemCode, string note = null) 
        {
            (Problems ??= new List<Problem>()).Add(new(new Identifier(Authorities.HL7, Dictionaries.HL7_0490_SampleRejectReasons, problemCode), note));
            return this;
        }

        public ValidationResult Validate()
        {
            return (_validator ??= new SampleValidator()).Validate(this);
        }

    }

}

