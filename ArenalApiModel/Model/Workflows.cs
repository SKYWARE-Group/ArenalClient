//Ignore Spelling: sco mcp pso 

namespace Skyware.Arenal.Model;


/// <summary>
/// Constants for Arenal-defined workflows
/// </summary>
public class Workflows
{

    /// <summary>
    /// Specimen Centric Orders.
    /// </summary>
    public const string LAB_SCO = "lab-sco";

    /// <summary>
    /// Mobile Collection Point.
    /// </summary>
    public const string LAB_MCP = "lab-mcp";

    /// <summary>
    /// Post Sale Order (Lab to patient web/mobile sale).
    /// </summary>
    public const string LAB_PSO = "lab-pso";

    /// <summary>
    /// Order workflows which are placer and provider are the same party.
    /// </summary>
    public static string[] SELF_ORDERS = { 
        LAB_MCP, 
        LAB_PSO 
    };

    /// <summary>
    /// Order where two parties are involved - placer and provider.
    /// </summary>
    public static string[] TWO_PARTIES_ORDERS = { 
        LAB_SCO 
    };

}
