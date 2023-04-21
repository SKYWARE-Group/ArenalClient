namespace Skyware.Arenal.Filters;


/// <summary>
/// Represent a part of a <see cref="Filter"/>.
/// </summary>
public interface IFilterPart
{

    /// <summary>
    /// Logical operator to be applied to the previous expression part.
    /// </summary>
    LogicalOperators LogicalOperator { get; set; }

}
