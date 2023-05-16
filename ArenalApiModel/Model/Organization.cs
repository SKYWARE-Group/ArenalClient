using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skyware.Arenal.Model;


/// <summary>
/// Arenal participant (Medical Center, Laboratory, Health Insurance Fund, etc.).
/// </summary>
[DisplayColumn(nameof(Name))]
public class Organization : EntityBase
{

    /// <summary>
    /// List of <see cref="Identifier"/> (may be empty).
    /// </summary>
    public IEnumerable<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Official name of the organization such as 'Precisio Medical Laboratories Inc.'.
    /// </summary>
    [Display(GroupName = "Organization", ShortName = "Name", Name = "Full Name",
        Description = $"Full valid name of the organization.",
        Prompt = "Please, enter organization full name.")]
    public string Name { get; set; }

    /// <summary>
    /// Short name of the provider or brand name, e.g. 'Precisio'.
    /// </summary>
    [Display(GroupName = "Organization", ShortName = "Short Name", Name = "Short Name",
        Description = $"Short name of the provider or brand name.",
        Prompt = "Please, enter organization brand name.")]
    public string ShortName { get; set; }

    /// <summary>
    /// Approved roles in Arenal.
    /// </summary>
    [Display(GroupName = "Roles", ShortName = "Roles", Name = "Roles",
        Description = $"Roles assigned to the organization by Arenal.")]
    public IEnumerable<string> Roles { get; set; }

    /// <summary>
    /// Registered address of an organization (used for billing)
    /// </summary>
    [Display(GroupName = "Registered Address", ShortName = "Registered address", Name = "Registered address",
        Description = $"Registered office address of the organization, (used for billing).")]
    public Address RegisteredAddress { get; set; }

    /// <summary>
    /// Head office of an organization (used for mailing, shipping, etc.)
    /// </summary>
    [Display(GroupName = "Head Address", ShortName = "Head Office", Name = "Head office address",
        Description = $"Head office address of the organization, (used for mailing, shipping, etc.).")]
    public Address HeadOffice { get; set; }

    /// <summary>
    /// Base URL of the organization's ARES server.
    /// Null or empty value means no ARES is implemented.
    /// </summary>
    [Display(GroupName = "Organization", ShortName = "AresUrl", Name = "AresBaseUrl",
        Description = $"Base URL of the organization's ARES server (Null or empty value means no ARES is implemented).")]
    public string AresBaseUrl { get; set; }

    /// <summary>
    /// Base URL where notifications will be made (web hook).
    /// Null or empty value means no web hook handler is implemented.
    /// </summary>
    [Display(GroupName = "Organization", ShortName = "Web hook", Name = "Web hook URL",
        Description = $"Web hook address to recieve notifications from Arenal.")]
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
    [Display(GroupName = "Logo", ShortName = "Large logo", Name = "Large logo URL",
        Description = $"URL of the medium logo of the organization like 64x64, 128x64, etc.",
        Prompt = "Please, enter URL of the large logo of the organization.")]
    public string LargeLogoUrl { get; set; }

    /// <summary>
    /// URL to a medium logo (64, 72, 128)
    /// </summary>
    [Display(GroupName = "Logo", ShortName = "Medium logo", Name = "Medium logo URL",
        Description = $"URL of the medium logo of the organization like 64x64, 128x64, etc.",
        Prompt = "Please, enter URL of the medium logo of the organization.")]
    public string MediumLogoUrl { get; set; }

    /// <summary>
    /// URL to a small logo (16, 32)
    /// </summary>
    [Display(GroupName = "Logo", ShortName = "Small Logo", Name = "Small Logo URL",
        Description = $"URL of the small logo of the organization like 16x16, 32x32 etc.",
        Prompt = "Please, enter URL of the small logo of the organization.")]
    public string SmallLogoUrl { get; set; }


}
