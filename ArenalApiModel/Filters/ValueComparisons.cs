namespace Skyware.Arenal.Filters;


/// <summary>
/// Predicate operators (Unary only).
/// </summary>
public enum ValueComparisons : byte
{

    /// <summary>
    /// Equals (numeric, date, string)
    /// </summary>
    /// <remarks>
    /// When comparison is applied to string, it is case-insensitive
    /// </remarks>
    [FilterKeyword("eq")]
    Equals,

    /// <summary>
    /// Not equals (numeric, date, string)
    /// </summary>
    /// <remarks>
    /// When comparison is applied to string, it is case-insensitive
    /// </remarks>
    [FilterKeyword("ne")]
    NotEquals,

    /// <summary>
    /// Contains (applies to strings only)
    /// </summary>
    /// <remarks>
    /// Case-insensitive, with exact match (no placeholders)
    /// </remarks>
    [FilterKeyword("cn")]
    Contains,

    /// <summary>
    /// Less than (numeric, date)
    /// </summary>
    [FilterKeyword("gt")]
    GreaterThan,

    /// <summary>
    /// Less than (numeric, date)
    /// </summary>
    [FilterKeyword("ge")]
    GreaterOrEquals,

    /// <summary>
    /// Greater than (numeric, date)
    /// </summary>
    [FilterKeyword("lt")]
    LessThan,

    /// <summary>
    /// Greater than (numeric, date)
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

    /// <summary>
    /// Starts with a value (applies to strings only)
    /// </summary>
    /// <remarks>
    /// Case-insensitive, with exact match (no placeholders)
    /// </remarks>
    [FilterKeyword("sw")]
    StartsWith,

    /// <summary>
    /// Ends with a value (applies to strings only)
    /// </summary>
    /// <remarks>
    /// Case-insensitive, with exact match (no placeholders)
    /// </remarks>
    [FilterKeyword("ew")]
    EndsWith,

    //TODO: Add more comparisons and implement them in PredicateHelper
    //Candidates: Like, Matches

}
