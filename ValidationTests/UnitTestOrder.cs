using FluentValidation.Results;
using Skyware.Arenal.Model;
using Skyware.Arenal.Validation;

namespace ValidationTests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Almost everything is wrong
        /// </summary>
        [Test]
        public void EmptyOrder()
        {
            Order order = new();

            ValidationResult validationResult = new OrderValidator().Validate(order);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.IsValid, Is.False);
                Assert.That(validationResult.Errors, Has.Count.EqualTo(3));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Workflow) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Patient) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Services) && x.Severity == FluentValidation.Severity.Error));
            });
        }

        /// <summary>
        /// Lab to lab
        /// </summary>
        /// <remarks>
        /// Wrong are:
        ///   Patient (No names)
        ///   One service (missing Id)
        ///   Samples are missing
        /// </remarks>
        [Test]
        public void LabToLab_Order_NoSvc()
        {
            Order order = new(Workflows.LAB_SCO, new Patient(), new[] { new Service("14749-6", "Glucose"), new Service() });

            ValidationResult validationResult = new OrderValidator().Validate(order);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.IsValid, Is.False);
                Assert.That(validationResult.Errors, Has.Count.EqualTo(4));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Patient) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Order.Services)) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Samples) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.ProviderId) && x.Severity == FluentValidation.Severity.Error));
            });
        }

        /// <summary>
        /// Lab to lab
        /// </summary>
        /// <remarks>
        /// Problem:
        ///     Try to put Order's fields on publishing
        /// </remarks>
        [Test]
        public void LabToLab_Order_ProviderFileds()
        {
            Order order = new(
                Workflows.LAB_SCO,
                new Patient() { FamilyName = "Doe" },
                new[] {
                    new Service("14749-6", "Glucose") {
                        Problems = new[] {
                            new Problem() { Identifier = new Identifier("Unknown service code.") }
                        }
                    }
                },
                new[] {
                    new Sample("SER", null, "X456TR") {
                        Problems = new[] {
                            new Problem() { Identifier = new Identifier(Authorities.HL7, "0490", "RB") }
                        }
                    }
                })
            {
                ProviderId = "Something",
                ProviderNote = new Note("Hello"),
            };

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
        /// Valid
        /// </summary>
        [Test]
        public void LabToLab_Order_Ok()
        {
            Order order = new Patient("John", "Doe")
                .CreateOrder(Workflows.LAB_SCO, "AD-O-34ER")
                .AddService("14749-6", "Glucose")
                .AddSample("SER", null, "X456TR");

            ValidationResult validationResult = order.Validate();
            Assert.That(validationResult.IsValid, Is.True);
        }

    }
}