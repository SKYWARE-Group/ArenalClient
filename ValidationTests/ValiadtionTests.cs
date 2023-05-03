using FluentValidation.Results;
using Skyware.Arenal.Model;
using Skyware.Arenal.Validation;

namespace ValidationTests;

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

    /// <summary>
    /// Patient validation
    /// </summary>
    [Test]
    [TestCase("a", "b", null, ExpectedResult = true)] //OK
    [TestCase("ﺿ", "Ҵ", null, ExpectedResult = true)] //OK
    [TestCase(null, null, null, ExpectedResult = false)] //no names at all
    [TestCase("a", null, null, ExpectedResult = false)] //full name is too short
    [TestCase("John", null, "1845-01-05", ExpectedResult = false)] //Date of birth is too early
    [TestCase("John", null, "2050-01-05", ExpectedResult = false)] //Date of birth is too late
    [TestCase("ABCDEFGHIJKLMNOPQRZSUVWXYZABCDEFGHIJKLMNOPQRZSUVWXYZABCDEFGHIJKLMNOPQRZSUVWXYZABCDEFGHIJKLMNOPQRZSUVWXYZ", 
        null, null, ExpectedResult = false)] //Full name is too long
    public bool Patient_BasicTests(string given, string family, string dob)
    {
        DateTime? born = null;
        if (!string.IsNullOrWhiteSpace(dob)) born = DateTime.ParseExact(dob, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        Patient pat = new(given, family, born: born);
        ValidationResult res = pat.Validate();
        return res.IsValid;
    }

    /// <summary>
    /// Patient validation
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Number of contacts
    /// </remarks>
    [Test]
    public void Patient_WrongNumContacts()
    {
        Patient pat = new("John", "Doe");
        for (int i =  0; i < PersonBase.MAX_CONTACTS + 1; i++)
        {
            pat.AddPhone("123456789");
        }
        ValidationResult res = pat.Validate();
        Assert.That(res.IsValid, Is.False);
        Assert.That(res.Errors.First().PropertyName == nameof(PersonBase.Contacts));
    }

    /// <summary>
    /// Patient validation
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   Wrong authority of 1-st identifier
    /// </remarks>
    [Test]
    public void Patient_WrongIdAuth()
    {
        Patient pat = new Patient("John", "Doe")
            .AddIdentifier(Authorities.HL7, "123456");
        ValidationResult res1 = pat.Validate();
        Assert.That(res1.IsValid, Is.False);
        Assert.That(res1.Errors.First().PropertyName.Contains(nameof(Identifier.Authority)));
    }


    /// <summary>
    /// Patient validation
    /// </summary>
    /// <remarks>
    /// Wrong are:
    ///   None (should succeed)
    /// </remarks>
    [Test]
    public void PatientValid()
    {
        Patient pat = new(null, "Doe");
        ValidationResult res2 = pat.Validate();
        Assert.That(res2.IsValid, Is.True);
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
    public void LabToLab_Order_ProviderFileds()
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
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
        Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.Services));
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
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
        Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.Samples));
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
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors.First().PropertyName ==  nameof(Order.ProviderId));
        Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
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
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors.First().PropertyName == nameof(Order.ProviderId));
        Assert.That(validationResult.Errors.First().Severity == FluentValidation.Severity.Error);
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

    #endregion

}