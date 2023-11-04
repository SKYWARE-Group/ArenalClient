// Ignore Spelling: abc

using Skyware.Arenal.Model;

namespace ModelTests;

public class OperatorsTests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IdentifierOperators()
    {

        // instance to null, !=
        Assert.That(new Identifier(), Is.Not.EqualTo(null));

        // null to instance, !=
        Assert.IsTrue(null != new Identifier());

        // default instances, ==
        Assert.IsTrue(new Identifier() == new Identifier());

        // equal instances, ==
        Assert.IsTrue(new Identifier("123") == new Identifier("123"));

        // semantically equal instances (case does not make sense), ==
        Assert.That(new Identifier("ABC"), Is.EqualTo(new Identifier("abc")));

        // local to loinc, !=
        Assert.IsTrue(new Identifier(Authorities.LOCAL, null, "123") != new Identifier(Authorities.LOINC, null, "123"));

        // loinc to loinc ==
        Assert.IsTrue(new Identifier(Authorities.LOINC, null, "123") == new Identifier(Authorities.LOINC, null, "123"));

        // same authorities, dictionary to null, !=
        Assert.IsTrue(new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, "123") != new Identifier(Authorities.WHO, null, "123"));

        // same authorities, null to dictionary, !=
        Assert.IsTrue(new Identifier(Authorities.WHO, null, "123") != new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, "123"));

        // same authorities, different dictionaries, !=
        Assert.IsTrue(new Identifier(Authorities.WHO, Dictionaries.WHO_Icd11, "123") != new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, "123"));

    }

}
