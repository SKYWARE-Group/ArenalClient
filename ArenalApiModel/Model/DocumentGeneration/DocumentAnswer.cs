namespace Skyware.Arenal.Model.DocumentGeneration;


/// <summary>
/// Answer to document generation request.
/// </summary>
public class DocumentAnswer
{

    /// <summary>
    /// Unique number of the task in case of idempotent invocation.
    /// </summary>
    public string TaskId { get; set; }

    /// <summary>
    /// Base64 encoded data for generated document.
    /// </summary>
    public string Data { get; set; }

}
