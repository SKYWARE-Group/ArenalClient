namespace Skyware.Arenal.Filters;


/// <summary>
/// Logical operators between predicates or expressions.
/// </summary>
public enum LogicalOperators : byte
{

    /// <summary>
    /// Conjunction
    /// </summary>
    [FilterKeyword("~and")]
    And,

    /// <summary>
    /// Disjunction
    /// </summary>
    [FilterKeyword("~or")]
    Or

}
