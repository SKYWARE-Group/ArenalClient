using FluentValidation.Results;
using Skyware.Arenal.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests.ValidationTests
{
    internal class PatientBaseValidation
    {

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
            for (int i = 0; i < PersonBase.MAX_CONTACTS + 1; i++)
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

        [Test]
        public void DublicateEmailContact()
        {
            Patient patient = new();
            patient.AddEmail("someEmail@test.com")
                   .AddEmail("someEmail@test.com");

            ValidationResult res = patient.Validate();

            Assert.That(res.IsValid, Is.False);
            Assert.That(res.Errors.Last().PropertyName, Is.EqualTo(nameof(Patient.Contacts)));
        }

        [Test]
        public void ValidEmailContact()
        {
            Patient patient = new("first", "second");
            patient.AddEmail("someEmail2@test.com")
                   .AddEmail("someEmail@test.com");

            ValidationResult res = patient.Validate();

            Assert.That(res.IsValid, Is.True);
            Assert.That(res.Errors.Any(), Is.False);
        }

        [Test]
        public void DublicatePhoneContact()
        {
            Patient patient = new();
            patient.AddEmail("0899504782")
                   .AddEmail("0899504782");

            ValidationResult res = patient.Validate();

            Assert.That(res.IsValid, Is.False);
            Assert.That(res.Errors.Last().PropertyName, Is.EqualTo(nameof(Patient.Contacts)));
        }

        [Test]
        public void ValidPhoneContact()
        {
            Patient patient = new("first","second");
            patient.AddEmail("0899504782")
                   .AddEmail("0899504783");

            ValidationResult res = patient.Validate();

            Assert.That(res.IsValid, Is.True);
            Assert.That(res.Errors.Any(), Is.False);
        }

        [Test]
        public void ValidPhoneAndEmailContact()
        {
            Patient patient = new("first", "second");
            patient.AddEmail("0899504782")
                   .AddEmail("0899504783")
                   .AddEmail("someEmail2@test.com")
                   .AddEmail("someEmail@test.com");

            ValidationResult res = patient.Validate();

            Assert.That(res.IsValid, Is.True);
            Assert.That(res.Errors.Any(), Is.False);
        }


        [Test]
        public void NullContact()
        {
            Patient patient = new("first", "second");

            ValidationResult res = patient.Validate();

            Assert.That(res.IsValid, Is.True);
            Assert.That(res.Errors.Any(), Is.False);
        }
    }
}
