using System.Linq;

namespace Skyware.Arenal.Model;

/// <summary>
/// Extension methods
/// </summary>
public static class ExtensionMethods
{

    /// <summary>
    /// Indicates the order is locked for placer party.
    /// </summary>
    public static bool IsLockedForPlacer(this Order order) =>
        Workflows.SELF_ORDERS.Any(x => x == order.Workflow) || !OrderStatuses.LOCKED_FOR_PLACER_STATUSES.Any(x => x == order.Status);

}
