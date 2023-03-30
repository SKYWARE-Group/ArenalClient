using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Skyware.Arenal.Filters
{

    public class Filter : IFilterPart
    {

        /// <summary>
        /// Holds expression parts.
        /// </summary>
        public List<IFilterPart> Parts { get; set; } = new List<IFilterPart>();

        public Filter() { }

        public Filter(Predicate predicate) : this()
        {
            predicate.LogicalOperator = LogicalOperators.And;
            Parts.Add(predicate);
        }

        public Filter(string propertyName, ValueComparisons comparison, object value) : this()
        {
            Parts.Add(new Predicate(propertyName, comparison, value));
        }

        /// <inheritdoc/>
        public LogicalOperators LogicalOperator { get; set; } = LogicalOperators.And;

        public Filter And<T>(T value) where T : IFilterPart
        {
            value.LogicalOperator = LogicalOperators.And;
            Parts.Add(value);
            return this;
        }

        public Filter And(string propertyName, ValueComparisons comparison, object value)
        {
            Parts.Add(new Predicate(propertyName, comparison, value, LogicalOperators.And));
            return this;
        }

        public Filter Or(string propertyName, ValueComparisons comparison, object value)
        {
            Parts.Add(new Predicate(propertyName, comparison, value, LogicalOperators.Or));
            return this;
        }

        public Filter Or<T>(T value) where T : IFilterPart
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

        private static string ExpressionToString(IList<IFilterPart> parts)
        {
            StringBuilder bld = new StringBuilder();
            //index 0
            if (parts[0] is Predicate) bld.Append(parts[0].ToString());
                else bld.Append(ExpressionToString(((Filter)parts[0]).Parts));
            //every next index
            for (int ind = 1; ind < parts.Count; ind++)
            {
                if (parts[ind] is Predicate) bld.Append($"{parts[ind].LogicalOperator}{parts[ind]}");
                else bld.Append($"{parts[ind].LogicalOperator}({ExpressionToString(((Filter)parts[ind]).Parts)})");
            }
            return bld.ToString();
        }


    }



}
