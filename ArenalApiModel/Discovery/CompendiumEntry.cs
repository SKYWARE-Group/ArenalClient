using Skyware.Arenal.Model;
using System.Collections.Generic;

namespace Skyware.Arenal.Discovery;


/// <summary>
/// Pricelist item.
/// </summary>
public class CompendiumEntry : Identifier
{

    /// <summary>
    /// Official provider's price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Required samples for given service (laboratory).
    /// </summary>
    public IEnumerable<SampleType> ReuiredSampleTypes { get; set; }


}
