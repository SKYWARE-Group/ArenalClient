namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Predicate operators (Unary only).
    /// </summary>
    public enum ValueComparisons : byte
    {

        /// <summary>
        /// Equals (int, decimal, date, string)
        /// </summary>
        /// <remarks>
        /// When comparison is applied to string, it is case-insensitive
        /// </remarks>
        [FilterKeyword("eq")]
        Equals,

        /// <summary>
        /// Not equals (int, decimal, date, string)
        /// </summary>
        /// <remarks>
        /// When comparison is applied to string, it is case-insensitive
        /// </remarks>
        [FilterKeyword("ne")]
        NotEquals,

        /// <summary>
        /// Contains (applies only to string)
        /// </summary>
        /// <remarks>
        /// Case-insensitive, with exact match (no placeholders)
        /// </remarks>
        [FilterKeyword("cn")]
        Contains,

        /// <summary>
        /// Less than (int, decimal, date)
        /// </summary>
        [FilterKeyword("gt")]
        GreaterThan,

        /// <summary>
        /// Less than (int, decimal, date)
        /// </summary>
        [FilterKeyword("ge")]
        GreaterOrEquals,

        /// <summary>
        /// Greater than (int, decimal, date)
        /// </summary>
        [FilterKeyword("lt")]
        LessThan,

        /// <summary>
        /// Greater than (int, decimal, date)
        /// </summary>
        [FilterKeyword("le")]
        LessOrEquals,

        /// <summary>
        /// Is (applies only to null as a value)
        /// </summary>
        [FilterKeyword("is")]
        Is,

        /// <summary>
        /// Is (applies only to null as a value)
        /// </summary>
        [FilterKeyword("isnot")]
        IsNot,

        //TODO: Add more comparisons and implement them in PredicateHelper
        //Candidates: StartsWith, EndsWith, Like, Matches

    }

}
