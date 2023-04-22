using System.Collections.Generic;

namespace Skyware.Arenal.Model.Actions;


/// <summary>
/// Payload for changing status of an <see cref="Order"/>.
/// </summary>
public class OrderStatusRequest
{

    /// <summary>
    /// New status the order have to be switched to (required).
    /// </summary>
    public string NewStatus { get; set; } = OrderStatuses.IN_PROGRESS;

    /// <summary>
    /// Note from provider to the placer (optional).
    /// </summary>
    public Note ProviderNote { get; set; }

    /// <summary>
    /// Problems and discrepancies related to the <see cref="Order.Samples"/> in given order (optional).
    /// </summary>
    /// <remarks>
    /// When new status is <see cref="OrderStatuses.IN_PROGRESS_WITH_PROBLEMS"/> or similar,
    /// and there is a problems with one or more samples, this collection
    /// must contain a record for the sample problem(s).
    /// </remarks>
    public IEnumerable<SampleProblem> SampleProblems { get; set; }

    /// <summary>
    /// Problems and discrepancies related to the <see cref="Order.Services"/> in given order (optional).
    /// </summary>
    /// <remarks>
    /// When new status is <see cref="OrderStatuses.IN_PROGRESS_WITH_PROBLEMS"/> or similar,
    /// and there is a problems with one or more service, this collection
    /// must contain a record for the service problem(s).
    /// </remarks>
    public IEnumerable<ServiceProblem> ServiceProblems { get; set; }



}
