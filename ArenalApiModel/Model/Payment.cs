using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model;

/// <summary>
/// Represents payment trough payment service, such as Stripe, etc.
/// </summary>
public class Payment : EntityBase
{

    /// <summary>
    /// Account of the owner (the one who receives the money).
    /// </summary>
    public string AccountId { get; set; }

    /// <summary>
    /// Date and time (UTC) the payment has been made.
    /// </summary>
    public DateTime TransactionTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time the payment, according to local timezone (server).
    /// </summary>
    public DateTime LocalTransactionTime { get => TransactionTime.ToLocalTime(); }

    /// <summary>
    /// Total amount.
    /// </summary>
    public decimal Amount { get; set; } = 0m;

    /// <summary>
    /// The payment system, such as Stripes, etc.
    /// </summary>
    public string PaymentSystemId { get; set; } = PaymentSystems.STRIPE;

    /// <summary>
    /// Reference number of the transaction according to payment system.
    /// </summary>
    public string PaymentSystemReference { get; set; }

    /// <summary>
    /// Arenal Id of a placer (initiator) of the payment.
    /// </summary>
    public string PlacerId { get; set; }

    /// <summary>
    /// Reference placed from the reference to keep track of what is paid.
    /// Usually this will be Id of an <see cref="Order"/>, but could be anything known to the placer.
    /// </summary>
    public string PlacerReference { get; set; }

    /// <summary>
    /// The Payer.
    /// </summary>
    public Payer Payer { get; set; }

    /// <summary>
    /// Optional, items such are covered by this transaction.
    /// </summary>
    public IEnumerable<PaymentItem> Items { get; set; }

    /// <summary>
    /// Base64 encoded original response from the payment system.
    /// </summary>
    public string OriginalResponse { get; set; }

}
