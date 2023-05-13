﻿using FluentValidation.Results;
using Skyware.Arenal.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skyware.Arenal.Model;


/// <summary>
/// Medical order for laboratory examination/observation or service.
/// </summary>
public class Order : EntityBase
{

    private static OrderValidator _validator;

    /// <summary>
    /// Identifies Arenal workflow.
    /// </summary>
    [Display(GroupName = "Header", ShortName = "Workflow", Name = "Workflow", 
        Description = "Defines the business logic for this Order. See the documentation for further details about the supported workflows.", 
        Prompt = "Please, enter a valid Workflow.")]
    public string Workflow { get; set; }

    /// <summary>
    /// ArenalId of the ordering party. Set by Arenal.
    /// </summary>
    [Display(GroupName = "Orderer", ShortName = "Placer Id", Name = "Arenal Id of the orderer",
        Description = "Identifies the ordering party by its Arenal Id.", 
        Prompt = "Please, set a valid Orderer.")]
    public string PlacerId { get; set; }

    /// <summary>
    /// ArenalId of the service provider to which this order is intended (conditional).
    /// In 'open' workflows, the identifier is generated by the party which takes the order.
    /// </summary>
    public string ProviderId { get; set; }

    /// <summary>
    /// Placer's order identifier (optional). This is the identifier generated by the party which places the order.
    /// </summary>
    public string PlacerOrderId { get; set; }

    /// <summary>
    /// Filler's order identifier (optional). This is the identifier generated by the party which consumes the order.
    /// </summary>
    public string ProviderOrderId { get; set; }

    /// <summary>
    /// Date and time the order was created (UTC).
    /// </summary>
    [Display(GroupName = "Header", ShortName = "Created", Name = "Date and time (UTC) of placing Order",
        Description = "Date and time (UTC) when the Order is placed in the Arenal by the Orderer (read only).")]
    public DateTime? Created { get; set; }

    /// <summary>
    /// Date and time the order was created (Local date and time).
    /// </summary>
    [Display(GroupName = "Header", ShortName = "Created", Name = "Date and time (local timezone) of placing Order",
        Description = "Date and time (local timezone) when the Order is placed in the Arenal by the Orderer (read only).")]
    public DateTime? LocalCreated { get => Created?.ToLocalTime(); }

    /// <summary>
    /// Date and time the order was last modified (UTC).
    /// </summary>
    [Display(GroupName = "Header", ShortName = "Modified", Name = "Date and time (UTC) the Order was modified",
        Description = "Date and time (UTC) when the Order is modified by the Orderer (read only).")]
    public DateTime? Modified { get; set; }

    /// <summary>
    /// Date and time the order was last modified (Local date and time).
    /// </summary>
    [Display(GroupName = "Header", ShortName = "Modified", Name = "Date and time (local timezone) the Order was modified",
        Description = "Date and time (local timezone) when the Order is modified by the Orderer (read only).")]
    public DateTime? LocalModified { get => Modified?.ToLocalTime(); }

    /// <summary>
    /// Version (server generated), starts from 0 and increments on every update from the publisher side.
    /// </summary>
    [Display(GroupName = "Header", ShortName = "Version", Name = "Version of the Order",
        Description = "Version is an integer value which is incremented and every change of the Order, starting at 0 (red only).")]
    public int Version { get; set; } = 0;

    /// <summary>
    /// Date and time when the order is taken or rejected by the provider (UTC).
    /// </summary>
    public DateTime? TakenOrRejected { get; set; }

    /// <summary>
    /// Date and time when the order is taken or rejected by the provider (Local date and time).
    /// </summary>
    public DateTime? LocalTakenOrRejected { get => TakenOrRejected?.ToLocalTime(); }

    /// <summary>
    /// Order status, according to <see cref="OrderStatuses"/>.
    /// </summary>
    public string Status { get; set; } = OrderStatuses.AVAILABLE;

    /// <summary>
    /// Notes from the placer.
    /// </summary>
    public Note PlacerNote { get; set; }

    /// <summary>
    /// Notes from the provider.
    /// </summary>
    public Note ProviderNote { get; set; }

    /// <summary>
    /// Ordering doctor.
    /// </summary>
    public Doctor Doctor { get; set; }

    /// <summary>
    /// Patient.
    /// </summary>
    [Display(GroupName = "Patient", ShortName = "Patient", Name = "Patient",
        Description = "Reference to a person who is a subject of services or examinations.")]
    public Patient Patient { get; set; }

    /// <summary>
    /// Additional orders or referrals, which are part of his order and ares stored and processed in external systems.
    /// </summary>
    [Display(GroupName = "References", ShortName = "Linked referrals", Name = "Linked referrals",
        Description = "References to referrals, placed in an external service.")]
    public IList<LinkedReferral> LinkedReferrals { get; set; }

    /// <summary>
    /// Unique collection of requested examinations or observations.
    /// </summary>
    /// <remarks>
    /// Uniqueness is imposed on equality on <see cref="Service.ServiceId"/>.
    /// See <see cref="Identifier"/> for details.
    /// </remarks>
    [Display(GroupName = "Services", ShortName = "Services", Name = "List of ordered services",
        Description = "Non empty list of ordered services or examinations.", Prompt = "Please, add at least one service/examination.")]
    public IList<Service> Services { get; set; }

    /// <summary>
    /// Unique collection of provided samples (conditional).
    /// </summary>
    /// <remarks>
    /// Uniqueness is imposed on equality on <see cref="Sample.SampleId"/> (the barcode).
    /// </remarks>
    [Display(GroupName = "Samples", ShortName = "Samples", Name = "List of provided samples",
        Description = "Non empty, list of unique samples to be analyzed.", Prompt = "Please, add at least one sample.")]
    public IList<Sample> Samples { get; set; }

    /// <summary>
    /// Order related files.
    /// </summary>
    public IList<Attachment> Attachments { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Order() : base() { }

    /// <summary>
    /// Instantiates minimal valid order.
    /// </summary>
    /// <param name="workflow"></param>
    /// <param name="placerId"></param>
    /// <param name="patient"></param>
    /// <param name="services"></param>
    /// <param name="samples"></param>
    /// <param name="providerId"></param>
    public Order(string workflow, string placerId, Patient patient, IList<Service> services = null, IList<Sample> samples = null, string providerId = null) : this()
    {
        Workflow = workflow;
        Patient = patient;
        if (services is not null) Services = services;
        if (samples is not null) Samples = samples;
        if (!string.IsNullOrWhiteSpace(providerId)) ProviderId = providerId;
    }

    public Order SetPatient(Patient patient)
    {
        Patient = patient;
        return this;
    }

    /// <summary>
    /// Safely adds a <see cref="Sample"/> to the order.
    /// </summary>
    /// <param name="sample">A <see cref="Sample"/> to add</param>
    public Order AddSample(Sample sample)
    {
        Samples ??= new List<Sample>();
        Samples.Add(sample);
        return this;
    }

    /// <summary>
    /// Safely adds a <see cref="Sample"/> to the order.
    /// </summary>
    /// <param name="sampleTypeCode"></param>
    /// <param name="barcode"></param>
    /// <param name="taken"></param>
    /// <param name="note"></param>
    /// <param name="additiveCode"></param>
    public Order AddSample(string sampleTypeCode, string additiveCode, string barcode, DateTime? taken = null, string note = null)
    {
        Samples ??= new List<Sample>();
        Samples.Add(new Sample(sampleTypeCode, additiveCode, barcode, taken, note));
        return this;
    }

    /// <summary>
    /// Safely adds a <see cref="Service"/> to the order.
    /// </summary>
    /// <param name="service">A <see cref="Service"/> to add</param>
    public Order AddService(Service service)
    {
        Services ??= new List<Service>();
        Services.Add(service);
        return this;
    }

    /// <summary>
    /// Safely adds a <see cref="Service"/> to the order.
    /// </summary>
    /// <param name="serviceCode">LOINC code for service to add</param>
    /// <param name="name">Name of the service to add</param>
    /// <param name="note">A note to the service</param>
    public Order AddService(string serviceCode, string name = null, string note = null)
    {
        Services ??= new List<Service>();
        Services.Add(new Service(serviceCode, name, note));
        return this;
    }

    /// <summary>
    /// Validates the order against business logic.
    /// </summary>
    /// <returns></returns>
    public ValidationResult Validate()
    {
        return (_validator ??= new OrderValidator()).Validate(this);
    }

}

