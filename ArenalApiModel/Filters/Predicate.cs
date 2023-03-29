using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Atomic part of a filter in server and client applications.
    /// </summary>
    public class Predicate : IExpressionPart
    {

        /// <summary>
        /// Instantiates <see cref="Predicate"/>
        /// </summary>
        public Predicate() { }

        /// <summary>
        /// Instantiate <see cref="Predicate"/> with values.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparison"></param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        public Predicate(string propertyName, ValueComparisons comparison, object value, LogicalOperators op = LogicalOperators.And ) : this()
        { 
            PropertyName = propertyName;
            ValueComparison = comparison;
            Value = value;
            LogicalOperator = op;
        }

        /// <inheritdoc/>
        public LogicalOperators LogicalOperator { get; set; } = LogicalOperators.And;

        /// <summary>
        /// Name of the property to be compared.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// ValueComparison operator to be applied between property and value.
        /// </summary>
        public ValueComparisons ValueComparison { get; set; } = ValueComparisons.Equals;

        /// <summary>
        /// Value for comparison.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Constructs query parameter part.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //TODO: Convert function for operator syntax
            //TODO: Convert value according to underlying type ('string', 5, 5.23, 2023-03-01, 2023-05-05T12:22:30, etc.)
            return $"({PropertyName},{ValueComparison},{PredicateHelper.GetPredicateValue(Value)})";
        }

    }

}
