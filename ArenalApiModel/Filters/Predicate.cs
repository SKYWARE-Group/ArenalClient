using Skyware.Arenal.Model;

namespace Skyware.Arenal.Filters;


/// <summary>
/// Atomic part of a filter in server and client applications.
/// </summary>
public class Predicate : IFilterPart
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
    public Predicate(string propertyName, ValueComparisons comparison, object value, LogicalOperators op = LogicalOperators.And) : this()
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
    /// Comparison operator to be applied between property and value.
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
        return $"({PropertyName.ToLower()},{PredicateHelper.GetPredicateComparison(ValueComparison)},{PredicateHelper.GetPredicateValue(Value)})";
    }

    #region Factory methods

    /// <summary>
    /// Creates predicate for searching orders by patient's identifier
    /// </summary>
    /// <param name="value"></param>
    /// <param name="comp"></param>
    /// <returns></returns>
    public static Predicate OrdersByPid(string value, ValueComparisons comp = ValueComparisons.Contains) =>
        new() {
            PropertyName = $"{nameof(Order.Patient)}.{nameof(Patient.Identifiers)}_.{nameof(Identifier.Value)}",
            ValueComparison = comp,
            Value = value
        };

    /// <summary>
    /// Creates predicate for searching orders by sample barcode
    /// </summary>
    /// <param name="value"></param>
    /// <param name="comp"></param>
    /// <returns></returns>
    public static Predicate OrdersBySampleId(string value, ValueComparisons comp = ValueComparisons.Equals) =>
        new()
        {
            PropertyName = $"{nameof(Order.Samples)}_.{nameof(Sample.SampleId)}.{nameof(Identifier.Value)}",
            ValueComparison = comp,
            Value = value
        };


    #endregion

}
