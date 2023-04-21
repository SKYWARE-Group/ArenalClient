namespace Skyware.Arenal.Model.Actions;


/// <summary>
/// Payload for changing status of an <see cref="Order"/>.
/// </summary>
public class OrderStatusRequest
{

    /// <summary>
    /// New status the order have to be switched to.
    /// </summary>
    public string NewStatus { get; set; } = OrderStatuses.IN_PROGRESS;

    /// <summary>
    /// Note from provider to the placer.
    /// </summary>
    public Note ProviderNote { get; set; }

    /// <summary>
    /// Non-null if new status is <see cref="OrderStatuses.REJECTED"/>
    /// </summary>
    public Identifier RejectReason { get; set; }

}
