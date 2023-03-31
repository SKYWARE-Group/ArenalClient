using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Predicate operators (Unary only).
    /// </summary>
    public enum ValueComparisons : byte
    {

        /// <summary>
        /// Equals
        /// </summary>
        [FilterKeyword("eq")]
        Equals,

        /// <summary>
        /// Not equals
        /// </summary>
        [FilterKeyword("ne")]
        NotEquals,

        /// <summary>
        /// Contains (string)
        /// </summary>
        [FilterKeyword("c")]
        Contains,

        /// <summary>
        /// Less than (int, decimal, date)
        /// </summary>
        [FilterKeyword("gt")]
        GreaterThan,

        /// <summary>
        /// Greater than (int, decimal, date)
        /// </summary>
        [FilterKeyword("lt")]
        LessThan,

        //TODO: Add more comparisons and implement them in PredicateHelper

    }

}
