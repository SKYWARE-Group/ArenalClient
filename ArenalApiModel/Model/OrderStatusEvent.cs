namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Payload for changing status of an <see cref="Order"/>.
    /// </summary>
    public class OrderStatusEvent
    {

        /// <summary>
        /// New status the order have to be switched to.
        /// </summary>
        public string NewStatus { get; set; } = OrderStatuses.TAKEN;

        /// <summary>
        /// Visit/Encounter/Order in provider's system.
        /// </summary>
        public string ProviderId { get; set; }

        /// <summary>
        /// Notes from provider to the placer.
        /// </summary>
        public string ProviderNote { get; set; }

    }

}
