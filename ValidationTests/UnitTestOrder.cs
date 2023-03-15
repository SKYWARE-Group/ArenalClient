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
        /// Completely empty Order
        /// </summary>
        [Test]
        public void EmptyOrder()
        {
            Order order = new();

            ValidationResult validationResult = new OrderValidator().Validate(order);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.IsValid, Is.False);
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Workflow) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Patient) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Sevrices) && x.Severity == FluentValidation.Severity.Error));
            });
        }

        /// <summary>
        /// Lab to lab: Order with missing samples
        /// </summary>
        [Test]
        public void LabToLab_Order_NoSvc()
        {
            Order order = new() { 
                Workflow = Workflows.LAB_SPM_ORD,
                Patient = new() { },
                Sevrices = new[] { new Service("14749-6", "Glucose"), new Service() }
            };
            
            ValidationResult validationResult = new OrderValidator().Validate(order);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.IsValid, Is.False);
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Patient) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName.Contains(nameof(Order.Sevrices)) && x.Severity == FluentValidation.Severity.Error));
                Assert.That(validationResult.Errors.Any(x => x.PropertyName == nameof(Order.Samples) && x.Severity == FluentValidation.Severity.Error));
            });
        }

    }
}