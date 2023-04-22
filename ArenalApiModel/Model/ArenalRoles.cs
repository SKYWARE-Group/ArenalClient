namespace Skyware.Arenal.Model;


/// <summary>
/// Constants for roles in Arenal.
/// </summary>
public class ArenalRoles
{

    /// <summary>
    /// Laboratory, specimen-centric orders - place orders
    /// </summary>
    public const string LAB_SCO_PLACER = Workflows.LAB_SCO + "-pub";

    /// <summary>
    /// Laboratory, specimen-centric orders - consume orders
    /// </summary>
    public const string LAB_SCO_PROVIDER = Workflows.LAB_SCO + "-prv";

}
