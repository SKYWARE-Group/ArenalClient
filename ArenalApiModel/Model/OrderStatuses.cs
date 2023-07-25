//Ignore Spelling: ip pa ok pp rj

namespace Skyware.Arenal.Model;


/// <summary>
/// Contains constant values for Order status
/// </summary>
public class OrderStatuses
{

    /// <summary>
    /// Order is placed and available
    /// </summary>
    public const string AVAILABLE = "pa";

    /// <summary>
    /// Order is in progress (taken by the provider) with no problems
    /// </summary>
    public const string IN_PROGRESS = "ip";

    /// <summary>
    /// Order is in progress, but with some discrepancies (actions are required)
    /// </summary>
    public const string IN_PROGRESS_WITH_PROBLEMS = "pp";

    /// <summary>
    /// Order is complete, no problems are found and results are available
    /// </summary>
    public const string COMPLETE = "ok";

    /// <summary>
    /// Order is complete and results are available, but with some problems
    /// </summary>
    public const string COMPLETE_WITH_PROBLEMS = "op";

    /// <summary>
    /// Order is completely rejected by the assigned provider
    /// </summary>
    public const string REJECTED = "rj";

}
