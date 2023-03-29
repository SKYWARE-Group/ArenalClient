using System.Collections.Generic;

namespace Skyware.Arenal.Filters
{

    public class FilterExpression : IExpressionPart
    {

        /// <summary>
        /// Holds expression parts.
        /// </summary>
        public List<IExpressionPart> Parts { get; set; } = new List<IExpressionPart>();

        public FilterExpression() { }

        public FilterExpression(Predicate predicate) : this()
        {
            predicate.LogicalOperator = LogicalOperators.And;
            Parts.Add(predicate);
        }

        /// <inheritdoc/>
        public LogicalOperators LogicalOperator { get; set; } = LogicalOperators.And;

        public FilterExpression And<T>(T value) where T : IExpressionPart
        {
            value.LogicalOperator = LogicalOperators.And;
            Parts.Add(value);
            return this;
        }

        public FilterExpression Or<T>(T value) where T : IExpressionPart
        {
            value.LogicalOperator = LogicalOperators.Or;
            Parts.Add(value);
            return this;
        }

        /// <summary>
        /// Constructs query parameter value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

    }



}
