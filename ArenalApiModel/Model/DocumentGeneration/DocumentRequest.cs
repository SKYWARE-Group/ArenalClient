namespace Skyware.Arenal.Model.DocumentGeneration;


/// <summary>
/// Represents a request for document (form) generation.
/// </summary>
public class DocumentRequest
{

    /// <summary>
    /// Type of the requested documents (Lab referral, etc).
    /// </summary>
    /// <example>bg.nhif.lab-referral</example>
    public string DocumentType { get; set; }

    /// <summary>
    /// Requested format of a requested document.
    /// </summary>
    /// <example>PDF</example>
    public string DocumentFormat { get; set; } = "PDF";

    /// <summary>
    /// Base64 encoded data for document generation.
    /// </summary>
    /// <example>eyJucm4iOiAiMjMwMDA0NTYxMEExIn0=</example>
    public string Data { get; set; }

}
