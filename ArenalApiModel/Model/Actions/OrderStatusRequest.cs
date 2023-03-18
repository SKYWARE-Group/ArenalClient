namespace Skyware.Arenal.Model.Actions
{

    /// <summary>
    /// Payload for changing status of an <see cref="Order"/>.
    /// </summary>
    public class OrderStatusRequest
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
        /// Note from provider to the placer.
        /// </summary>
        public Note ProviderNote { get; set; }

    }

}
