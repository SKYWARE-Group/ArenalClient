using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Filters
{

    public static class PredicateHelper
    {

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

    }

}
