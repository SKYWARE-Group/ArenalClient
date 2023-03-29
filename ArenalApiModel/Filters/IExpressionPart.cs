namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Represent a part of a <see cref="FilterExpression"/>.
    /// </summary>
    public interface IExpressionPart
    {

        /// <summary>
        /// Logical operator to be applied to the previous expression part.
        /// </summary>
        LogicalOperators LogicalOperator { get; set; }

    }

}
