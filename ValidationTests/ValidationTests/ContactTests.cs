using Skyware.Arenal.Model;
using Skyware.Arenal.Validation;

namespace ModelTests.ValidationTests
{
    [TestFixture]
    public class ContactTests
    {
        [Test]
        [TestCase("konstantin.kirov.atanasov@gmail.com")]
        [TestCase("testEmail@mail.bg")]
        [TestCase("another_testEmail001@yahoo.com")]
        [TestCase("test00-tst@gmail.com")]
        public void Valid_Email_Assumption(string email)
        {
            Contact contact = new(ContactTypes.EMAIL, email);
            ContactValidator sut = new();

            Assert.IsTrue(sut.Validate(contact).IsValid);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("@")]
        [TestCase("@.")]
        [TestCase("test00-tst@gmaila")]
        [TestCase("t@@gmail.com")]
        [TestCase("test00-tst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmail@gmail.com")]
        [TestCase("test00-tsttst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmailtst_TooooLongEmail")]
        [TestCase("test00-tst@test00-tst")]
        public void InValid_Email_Assumption(string email)
        {
            Contact contact = new(ContactTypes.EMAIL, email);
            ContactValidator sut = new();

            Assert.IsFalse(sut.Validate(contact).IsValid);
        }

        [Test]
        [TestCase("0895641785")]
        [TestCase("0895 641 785")]
        [TestCase("0895-641-785")]
        [TestCase("0895-641 785")]
        [TestCase("0895 641-785")]
        [TestCase("0895 641(785)")]
        [TestCase("0895(641)785")]
        [TestCase("+359895641785")]
        [TestCase("+359 895 641 785")]
        [TestCase("+359-895-641-785")]
        [TestCase("+359(895)641 785")]
        [TestCase("+359 (895) 641 785")]
        [TestCase("+359-895 641 785")]
        [TestCase("+359 895-641 785")]
        [TestCase("00359895641785")]
        public void Valid_Phone_Assumption(string phone)
        {
            Contact contact = new(ContactTypes.PHONE, phone);
            ContactValidator sut = new();

            Assert.IsTrue(sut.Validate(contact).IsValid);
        }

        [Test]
        [TestCase(null)]
        [TestCase("123")]
        [TestCase("112")]
        [TestCase("4686873535465435434")]
        [TestCase("8237113")]
        [TestCase("8237-113")]
        [TestCase("00-257-489-655-44-666")]
        [TestCase("0025748965544002574896554400257489655440025748965544002574896554400257489655440025748965544002574896554400257489655440025748965544002574896554400257489655440025748965544")]
        public void InValid_Phone_Assumption(string phone)
        {
            Contact contact = new(ContactTypes.PHONE, phone);
            ContactValidator sut = new ContactValidator();

            Assert.IsFalse(sut.Validate(contact).IsValid);
        }
    }
}
