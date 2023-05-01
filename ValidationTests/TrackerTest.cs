using Skyware.Arenal.Model;
using Skyware.Arenal.Tracking;

namespace ValidationTests;

public class TrackerTest
{
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Order -> non-null to null
    /// </summary>
    [Test]
    public void OrderToNull()
    {
        Order orderA = new();

        EntityChange[] changes = orderA.CompareTo(null, nameof(Order)).ToArray();
        Assert.That(changes, Has.Length.EqualTo(1));
        Assert.That(changes.First().ChangeType, Is.EqualTo(EntityChange.ChangeTypes.Changed));
    }

    /// <summary>
    /// Order.Patient -> null to non-null
    /// </summary>
    [Test]
    public void OrderPatToNonNull()
    {
        Order orderA = new();
        Order orderB = new() { Patient = new() { GivenName = "John" } };

        EntityChange[] changes = orderA.CompareTo(orderB, nameof(Order)).ToArray();
        Assert.That(changes, Has.Length.EqualTo(1));
    }

    /// <summary>
    /// Order.Patient.GivenName -> "John" to "Jane"
    /// </summary>
    [Test]
    public void OrderPatChangedName()
    {
        Order orderA = new() { Patient = new() { GivenName = "John" } };
        Order orderB = new() { Patient = new() { GivenName = "Jane" } };

        EntityChange[] changes = orderA.CompareTo(orderB, nameof(Order)).ToArray();
        Assert.That(changes, Has.Length.EqualTo(1));
    }

    /// <summary>
    /// Order.Services -> null to empty
    /// </summary>
    [Test]
    public void OrderServicesBasic()
    {
        Order orderA = new() { Patient = new() { GivenName = "John" } };
        Order orderB = new() { Patient = new() { GivenName = "John" }, Services = Array.Empty<Service>().ToList() };

        EntityChange[] changes = orderA.CompareTo(orderB, nameof(Order)).ToArray();
        Assert.That(changes, Has.Length.EqualTo(1));
    }

    [Test]
    public void _UNFINISHED_Test()
    {

        //List to null
        List<Service> services = new() { new Service("123", "One") };
        EntityChange[] changes1 = services.CompareTo(null, nameof(Order)).ToArray();
        Assert.That(changes1, Has.Length.EqualTo(1));

        //List to changed list
        List<Service> trg = new() { new Service("123", "Two") };
        EntityChange[] changes2 = services.CompareTo(trg, nameof(Order)).ToArray();
        Assert.That(changes2, Has.Length.EqualTo(1));

    }



}
