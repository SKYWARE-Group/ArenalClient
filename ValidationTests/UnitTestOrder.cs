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
        /// Everything is wrong
        /// </summary>
        [Test]
        public void EmptyOrder()
        {
            Order order = new();

            ValidationResult validationResult = new OrderValidator().Validate(order);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.IsValid, Is.False);
                Assert.That(validationResult.Errors.Count == 3);
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
            Order order = new(Workflows.LAB_SPM_ORD, new Patient(), new[] { new Service("14749-6", "Glucose"), new Service() });

            ValidationResult validationResult = new OrderValidator().Validate(order);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.IsValid, Is.False);
                Assert.That(validationResult.Errors.Count == 3);
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Patient) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Order.Services)) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Samples) && x.Severity == FluentValidation.Severity.Error));
            });
        }

    }
}