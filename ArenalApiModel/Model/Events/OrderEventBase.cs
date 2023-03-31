namespace Skyware.Arenal.Model.Events
{

    /// <summary>
    /// Base (abstract) class for Order-related events.
    /// </summary>
    public abstract class OrderEventBase
    {

        public string OrderId { get; set; }

    }

}
