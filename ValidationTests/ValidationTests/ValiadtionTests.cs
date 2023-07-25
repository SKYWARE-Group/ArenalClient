//Ignore Spelling: pso mcp svc 

using FluentValidation.Results;
using Skyware.Arenal;
using Skyware.Arenal.Model;
using Skyware.Arenal.Validation;
using System.Security.Cryptography.X509Certificates;

namespace ModelTests.ValidationTests;

public class Tests
{

    [SetUp]
    public void Setup()
    {
    }

    #region Identifiers

    /// <summary>
    /// Identifier validations
    /// </summary>
    [Test]
    [TestCase(null, null, null, ExpectedResult = false)] //missing authority and value
    [TestCase(null, null, "123", ExpectedResult = false)] //missing authority
    [TestCase(Authorities.HL7, null, null, ExpectedResult = false)] //missing value
    [TestCase(Authorities.HL7, Dictionaries.WHO_Icd10, "SER", ExpectedResult = false)] //wrong dictionary
    [TestCase("ABCDEFGHIJKLMNOPQRSTO", null, "123", ExpectedResult = false)] //Authority is larger than allowed
    [TestCase("ABC", null, "ABCDEFGHIJKLMNOPQRSTABCDEFGHIJKLMNOPQRSTABCDEFGHIJKLMNOPQRST", ExpectedResult = false)] //Value is larger than allowed
    public bool IdentifierValidations(string auth, string dict, string value)
    {
        Identifier id = new(auth, dict, value);

        ValidationResult validationResult = id.Validate();
        return validationResult.IsValid;
    }

    #endregion

    #region Patients

    #endregion

    #region Services

    /// <summary>
    /// Valid service
    /// </summary>
    [Test]
    public void ServiceValid()
    {
        Service svc = new("14749-6", "Glucose");

        ValidationResult validationResult = svc.Validate();
        Assert.That(validationResult.IsValid, Is.True);
        Assert.That(validationResult.Errors, Has.Count.EqualTo(0));
    }

    /// <summary>
    /// Wrong is:
    ///     Name (too long)
    /// </summary>
    [Test]
    public void ServiceNotValidName()
    {
        Service svc = new("14749-6", "Glucose".Repeat(50));

        ValidationResult validationResult = svc.Validate();
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors.First().PropertyName == nameof(Service.Name));
    }

    /// <summary>
    /// Wrong is:
    ///     First alternate identifier
    /// </summary>
    [Test]
    public void ServiceRepeatedAltId()
    {
        Service svc = new Service("14749-6", "Glucose")
            .AddAlternateIdentifier("a", "a", "a");

        ValidationResult validationResult = svc.Validate();
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors.Any(x => x.PropertyName.StartsWith(nameof(Service.AlternateIdentifiers)) && x.Severity == FluentValidation.Severity.Error));
    }


    [Test]
    [TestCase("", ExpectedResult = false)] //Empty note, invalid
    [TestCase("a", ExpectedResult = false)] //Too short note, invalid
    [TestCase("Note", ExpectedResult = true)] //Valid
    [TestCase("x", ExpectedResult = false)] //Too long note ('x' will be repeated)
    public bool ServiceWrongNote(string note)
    {

        if (!string.IsNullOrEmpty(note) && note == "x") { note = note.Repeat(Note.MAX_LEN + 1); }
        Service svc = new Service("14749-6", "Glucose") { Note = new(note) };

        ValidationResult validationResult = svc.Validate();
        return validationResult.IsValid;
    }

    #endregion

    #region Orders

    /// <summary>
    /// Almost everything is wrong
    /// </summary>
    [Test]
    public void EmptyOrder()
    {
        Order order = new();

        ValidationResult validationResult = order.Validate();

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors, Has.Count.EqualTo(4));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.PlacerId) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Workflow) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Patient) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Services) && x.Severity == FluentValidation.Severity.Error));
        });
    }

    /// <summary>
    /// LAB_SCO
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   No placer id
    ///   No provider id
    ///   Patient (No names)
    ///   One service with missing Id
    ///   No samples (mandatory in LAB_SCO)
    /// </remarks>
    [Test]
    public void LabToLab_Order_NoProv_WrongSvc()
    {

        Order order = new Patient()
            .CreateOrder(Workflows.LAB_SCO, null, null)
            .AddService("14749-6", "Glucose")
            .AddService(new Service());

        ValidationResult validationResult = new OrderValidator().Validate(order);

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors, Has.Count.EqualTo(5));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.PlacerId) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Patient.FullName)) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Order.Services)) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Samples) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.ProviderId) && x.Severity == FluentValidation.Severity.Error));
        });
    }

    /// <summary>
    /// LAB_SCO
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Publisher sets provider's note
    ///   Publisher adds sample problem
    ///   Publisher adds service problem
    /// </remarks>
    [Test]
    public void LabToLab_Order_ProviderFields()
    {

        Order order = new Patient(null, "Doe")
            .CreateOrder(Workflows.LAB_SCO, "AD-G-1", "AD-P-1")
            .AddService("14749-6", "Glucose")
            .AddSample("SER", null, "X456TR");

        order.Services.First().AddProblem("Unknown service identifier.");

        //RB: Broken container
        order.Samples.First().AddProblem(new Problem(new Identifier(Authorities.HL7, Dictionaries.HL7_0490_SampleRejectReasons, "RB")));

        order.ProviderNote = new Note("Hello");

        ValidationResult validationResult = new OrderValidator().Validate(order);

        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors, Has.Count.EqualTo(3));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.ProviderNote) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Order.Services)) && x.Severity == FluentValidation.Severity.Error));
            Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Order.Samples)) && x.Severity == FluentValidation.Severity.Error));
        });
    }

    /// <summary>
    /// LAB_SCO
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Service duplication
    /// </remarks>
    [Test]
    public void LabToLab_DuplicateServices()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_SCO, "AD-G-1", "AD-O-34ER")
            .AddService("14749-6", "Glucose")
            .AddService("14749-6", "Глюкоза")
            .AddSample("SER", null, "X456TR");

        ValidationResult validationResult = order.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
            Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.Services));
        });
    }

    /// <summary>
    /// LAB_SCO
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Sample duplication
    /// </remarks>
    [Test]
    public void LabToLab_DuplicateSamples()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_SCO, "AD-G-1", "AD-O-34ER")
            .AddService("14749-6", "Glucose")
            .AddSample("SER", null, "X456TR")
            .AddSample("SER", null, "X456TR");

        ValidationResult validationResult = order.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
            Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.Samples));
        });
    }

    /// <summary>
    /// LAB_SCO
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Placer and provider are the same
    /// </remarks>
    [Test]
    public void LabToLab_SameProvider()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_SCO, "AD-G-1", "AD-G-1")
            .AddService("14749-6", "Glucose")
            .AddSample("SER", null, "X456TR");

        ValidationResult validationResult = order.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.ProviderId));
            Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
        });
    }

    /// <summary>
    /// LAB_MCP
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Provider has value (and shouldn't)
    /// </remarks>
    [Test]
    public void LabToLab_ProviderInMcp()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_MCP, "AD-G-1", "AD-G-1")
            .AddService("14749-6", "Glucose")
            .AddSample("SER", null, "X456TR");

        ValidationResult validationResult = order.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.ProviderId));
            Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
        });
    }

    /// <summary>
    /// LAB_SCO: Valid order
    /// </summary>
    [Test]
    public void LabToLab_Order_Ok()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_SCO, "AD-G-1", "AD-O-34ER")
            .AddService("14749-6", "Glucose")
            .AddSample("SER", null, "X456TR");

        ValidationResult validationResult = order.Validate();
        Assert.That(validationResult.IsValid, Is.True);
    }

    /// <summary>
    /// LAB_SCO: Valid order
    /// </summary>
    [Test]
    public void LabToLab_Mcp_Order_Ok()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_MCP, "AD-G-1", null)
            .AddService("14749-6", "Glucose")
            .AddSample("SER", null, "X456TR");

        ValidationResult validationResult = order.Validate();
        Assert.That(validationResult.IsValid, Is.True);
    }

    /// <summary>
    /// LAB_PSO: Wrong is:
    ///     No expiration
    /// </summary>
    [Test]
    public void LabToPat_Pso_Order_No_Exp()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_PSO, "AD-G-1", null)
            .AddService("14749-6", "Glucose", "Some text here", 2.2m);
        order.PublicAccessEnabled = true;
        order.Expiration = null;

        ValidationResult validationResult = order.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.Expiration));
        });
    }

    /// <summary>
    /// LAB_PSO: Wrong is:
    ///     No end user price
    /// </summary>
    [Test]
    public void LabToPat_Pso_Order_No_Price()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_PSO, "AD-G-1", null)
            .AddService("14749-6", "Glucose", "Some text here");
        order.PublicAccessEnabled = true;
        order.Expiration = DateTime.UtcNow.AddHours(36);

        ValidationResult validationResult = order.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.Services));
            Assert.That(validationResult.Errors.First().ErrorMessage.Contains(nameof(Service.EndUserPrice)));
        });
    }

    /// <summary>
    /// LAB_PSO: Valid order
    /// </summary>
    [Test]
    public void LabToPat_Pso_Order_Ok()
    {
        Order order = new Patient("John", "Doe")
            .CreateOrder(Workflows.LAB_PSO, "AD-G-1", null)
            .AddService("14749-6", "Glucose", "Some text here", 2.2m);
        order.PublicAccessEnabled = true;
        order.Expiration = DateTime.UtcNow.AddHours(36);

        ValidationResult validationResult = order.Validate();
        Assert.That(validationResult.IsValid, Is.True);
    }



    #endregion

}