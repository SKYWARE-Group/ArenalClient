using System;

namespace Skyware.Arenal.ApiModel.Model;


/// <summary>
/// Metadata and non-plaintext verification material for an organization-owned API key.
/// </summary>
public class OrganizationApiKey
{

    /// <summary>
    /// Unique identifier of the API key record.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Client identifier or display name used to distinguish the API key.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Non-plaintext HMAC verification value for the API key.
    /// </summary>
    public string KeyHash { get; set; }

    /// <summary>
    /// Indicates whether the API key is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Date and time the API key record was created (UTC).
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Optional date and time the API key record expires (UTC).
    /// </summary>
    public DateTime? Expires { get; set; }

}
