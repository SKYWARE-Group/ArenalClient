using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Arenal participant (Medical Center, Laboratory, Health Insurance Fund, etc.).
/// </summary>
public class Organization : EntityBase
{

    /// <summary>
    /// List of <see cref="Identifier"/> (may be empty).
    /// </summary>
    public IEnumerable<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Official name of the organization such as 'Precisio Medical Laboratories Inc.'.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Short name of the provider or brand name, e.g. 'Precisio'.
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// Approved roles in Arenal.
    /// </summary>
    public IEnumerable<string> Roles { get; set; }

    /// <summary>
    /// Registered address of an organization (used for billing)
    /// </summary>
    public Address RegisteredAddress { get; set; }

    /// <summary>
    /// Head office of an organization (used for mailing, shipping, etc.)
    /// </summary>
    public Address HeadOffice { get; set; }

    /// <summary>
    /// Base URL of the organization's ARES server.
    /// Null or empty value means no ARES is implemented.
    /// </summary>
    public string AresBaseUrl { get; set; }

    /// <summary>
    /// Base URL where notifications will be made (web hook).
    /// Null or empty value means no web hook handler is implemented.
    /// </summary>
    public string WebHookBaseUrl { get; set; }

    /// <summary>
    /// Soft delete indicator (false=deleted).
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// To list in public catalog or not.
    /// </summary>
    public bool ListInCatalog { get; set; } = true;

    /// <summary>
    /// URL to a large logo (256, 512)
    /// </summary>
    public string LargeLogoUrl { get; set; }

    /// <summary>
    /// URL to a medium logo (64, 72, 128)
    /// </summary>
    public string MediumLogoUrl { get; set; }

    /// <summary>
    /// URL to a small logo (16, 32)
    /// </summary>
    public string SmallLogoUrl { get; set; }


}
