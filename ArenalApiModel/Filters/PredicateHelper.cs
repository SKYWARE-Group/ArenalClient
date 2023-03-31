using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Filter language helper.
    /// </summary>
    public static class PredicateHelper
    {

        private static readonly Dictionary<ValueComparisons, string> _comparisonsMap = CreateComparisonsMap();
        private static readonly Dictionary<LogicalOperators, string> _logicalOperatorsMap = CreateOperatorsMap();

        private static Dictionary<ValueComparisons, string> CreateComparisonsMap()
        {
            Dictionary<ValueComparisons, string> rv = new Dictionary<ValueComparisons, string>();
            foreach (ValueComparisons comp in Enum.GetValues(typeof(ValueComparisons)))
            {
                rv.Add(
                    comp,
                    typeof(ValueComparisons)
                        .GetField(Enum.GetName(typeof(ValueComparisons), comp))?
                        .GetCustomAttributes(false).OfType<FilterKeywordAttribute>()?
                        .FirstOrDefault()?.Keyword ?? string.Empty);
            }
            return rv;
        }

        private static Dictionary<LogicalOperators, string> CreateOperatorsMap()
        {
            Dictionary<LogicalOperators, string> rv = new Dictionary<LogicalOperators, string>();
            foreach (LogicalOperators comp in Enum.GetValues(typeof(LogicalOperators)))
            {
                rv.Add(
                    comp,
                    typeof(LogicalOperators)
                        .GetField(Enum.GetName(typeof(LogicalOperators), comp))?
                        .GetCustomAttributes(false).OfType<FilterKeywordAttribute>()?
                        .FirstOrDefault()?.Keyword ?? string.Empty);
            }
            return rv;
        }


        /// <summary>
        /// Converts object to Arenal syntax string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetPredicateValue(object value)
        {
            if (value is null) return "null";
            else if (value is string) return $"'{value}'";
            else if (value is char @char) return $"'{@char}'";
            else if (value is sbyte || value is short || value is int || value is long) return Convert.ToInt64(value).ToString("G", System.Globalization.CultureInfo.InvariantCulture);
            else if (value is byte || value is ushort || value is uint || value is ulong) return Convert.ToUInt64(value).ToString("G", System.Globalization.CultureInfo.InvariantCulture);
            else if (value is double || value is decimal || value is float) return Convert.ToDecimal(value).ToString("G", System.Globalization.CultureInfo.InvariantCulture);
            else if (value is DateTime time) return time.ToString(time.TimeOfDay.TotalSeconds > 0 ? "yyyy-MM-ddThh:mm:ss" : "yyyy-MM-dd");
            else throw new ArgumentOutOfRangeException("Unsupported value type for predicate.");
        }

        /// <summary>
        /// Converts predicate comparison to Arenal string.
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetPredicateComparison(ValueComparisons comparison)
        {
            if (_comparisonsMap.TryGetValue(comparison, out var v)) return v; else return string.Empty;
        }

        /// <summary>
        /// Converts logical operator to Arenal string.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static string GetLogicalOperator(LogicalOperators op)
        {
            if (_logicalOperatorsMap.TryGetValue(op, out var v)) return v; else return string.Empty;
        }


    }

}
