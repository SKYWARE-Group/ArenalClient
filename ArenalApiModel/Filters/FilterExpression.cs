using System.Collections.Generic;
using System.IO;
using System.Text;

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
            if ((Parts?.Count ?? 0) == 0) return null;
            return ExpressionToString(Parts);
        }

        private static string ExpressionToString(IList<IExpressionPart> parts)
        {
            StringBuilder bld = new StringBuilder();
            //index 0
            if (parts[0] is Predicate) bld.Append(parts[0].ToString());
                else bld.Append(ExpressionToString(((FilterExpression)parts[0]).Parts));
            //every next index
            for (int ind = 1; ind < parts.Count; ind++)
            {
                if (parts[ind] is Predicate) bld.Append($"{parts[ind].LogicalOperator}{parts[ind]}");
                else bld.Append($"{parts[ind].LogicalOperator}({ExpressionToString(((FilterExpression)parts[ind]).Parts)})");
            }
            return bld.ToString();
        }


    }



}
