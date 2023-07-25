using System.Collections.Generic;

namespace Skyware.Arenal.Model;

/// <summary>
/// A payable item, part of a <see cref="Payment"/>.
/// </summary>
public class PaymentItem
{

    /// <summary>
    /// List of identifiers of an item, such as product code, LOINC, etc.
    /// </summary>
    public IEnumerable<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Name, as it shown to the customer.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Quantity of products being sold.
    /// </summary>
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Price of the item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Currency (ISO 4217) of the transaction.
    /// </summary>
    public string Currency { get; set; }

}
