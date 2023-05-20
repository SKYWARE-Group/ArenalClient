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
    [Display(GroupName = nameof(L10n.Organization.Organization.IdentifiersGroupName),
        ShortName = nameof(L10n.Organization.Organization.IdentifiersShortName),
        Name = nameof(L10n.Organization.Organization.IdentifiersName),
        Description = nameof(L10n.Organization.Organization.IdentifiersDescription),
        ResourceType = typeof(L10n.Organization.Organization))]
    public IEnumerable<Identifier> Identifiers { get; set; }

    /// <summary>
    /// Official name of the organization such as 'Precisio Medical Laboratories Inc.'.
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.NameGroupName),
        ShortName = nameof(L10n.Organization.Organization.NameShortName),
        Name = nameof(L10n.Organization.Organization.NameName),
        Description = nameof(L10n.Organization.Organization.NameDescription),
        Prompt = nameof(L10n.Organization.Organization.NamePrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public string Name { get; set; }

    /// <summary>
    /// Short name of the provider or brand name, e.g. 'Precisio'.
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.ShortNameGroupName),
        ShortName = nameof(L10n.Organization.Organization.ShortNameShortName),
        Name = nameof(L10n.Organization.Organization.ShortNameName),
        Description = nameof(L10n.Organization.Organization.ShortNameDescription),
        Prompt = nameof(L10n.Organization.Organization.ShortNamePrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public string ShortName { get; set; }

    /// <summary>
    /// Approved roles in Arenal.
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.RolesGroupName),
        ShortName = nameof(L10n.Organization.Organization.RolesShortName),
        Name = nameof(L10n.Organization.Organization.RolesName),
        Description = nameof(L10n.Organization.Organization.RolesDescription),
        ResourceType = typeof(L10n.Organization.Organization))]
    public IEnumerable<string> Roles { get; set; }

    /// <summary>
    /// Registered address of an organization (used for billing)
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.RegisteredAddressGroupName),
        ShortName = nameof(L10n.Organization.Organization.RegisteredAddressShortName),
        Name = nameof(L10n.Organization.Organization.RegisteredAddressName),
        Description = nameof(L10n.Organization.Organization.RegisteredAddressDescription),
        Prompt = nameof(L10n.Organization.Organization.RegisteredAddressPrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public Address RegisteredAddress { get; set; }

    /// <summary>
    /// Head office of an organization (used for mailing, shipping, etc.)
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.HeadOfficeGroupName),
        ShortName = nameof(L10n.Organization.Organization.HeadOfficeShortName),
        Name = nameof(L10n.Organization.Organization.HeadOfficeName),
        Description = nameof(L10n.Organization.Organization.HeadOfficeDescription),
        Prompt = nameof(L10n.Organization.Organization.HeadOfficePrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public Address HeadOffice { get; set; }

    /// <summary>
    /// Base URL of the organization's ARES server.
    /// Null or empty value means no ARES is implemented.
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.AresBaseUrlGroupName),
        ShortName = nameof(L10n.Organization.Organization.AresBaseUrlShortName),
        Name = nameof(L10n.Organization.Organization.AresBaseUrlName),
        Description = nameof(L10n.Organization.Organization.AresBaseUrlDescription),
        Prompt = nameof(L10n.Organization.Organization.AresBaseUrlPrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public string AresBaseUrl { get; set; }

    /// <summary>
    /// Base URL where notifications will be made (web hook).
    /// Null or empty value means no web hook handler is implemented.
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.WebHookBaseUrlGroupName),
        ShortName = nameof(L10n.Organization.Organization.WebHookBaseUrlShortName),
        Name = nameof(L10n.Organization.Organization.WebHookBaseUrlName),
        Description = nameof(L10n.Organization.Organization.WebHookBaseUrlDescription),
        Prompt = nameof(L10n.Organization.Organization.WebHookBaseUrlPrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
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
    [Display(GroupName = nameof(L10n.Organization.Organization.LargeLogoUrlGroupName),
        ShortName = nameof(L10n.Organization.Organization.LargeLogoUrlShortName),
        Name = nameof(L10n.Organization.Organization.LargeLogoUrlName),
        Description = nameof(L10n.Organization.Organization.LargeLogoUrlDescription),
        Prompt = nameof(L10n.Organization.Organization.LargeLogoUrlPrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public string LargeLogoUrl { get; set; }

    /// <summary>
    /// URL to a medium logo (64, 72, 128)
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.MediumLogoUrlGroupName),
        ShortName = nameof(L10n.Organization.Organization.MediumLogoUrlShortName),
        Name = nameof(L10n.Organization.Organization.MediumLogoUrlName),
        Description = nameof(L10n.Organization.Organization.MediumLogoUrlDescription),
        Prompt = nameof(L10n.Organization.Organization.MediumLogoUrlPrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public string MediumLogoUrl { get; set; }

    /// <summary>
    /// URL to a small logo (16, 32)
    /// </summary>
    [Display(GroupName = nameof(L10n.Organization.Organization.SmallLogoUrlGroupName),
        ShortName = nameof(L10n.Organization.Organization.SmallLogoUrlShortName),
        Name = nameof(L10n.Organization.Organization.SmallLogoUrlName),
        Description = nameof(L10n.Organization.Organization.SmallLogoUrlDescription),
        Prompt = nameof(L10n.Organization.Organization.SmallLogoUrlPrompt),
        ResourceType = typeof(L10n.Organization.Organization))]
    public string SmallLogoUrl { get; set; }


}
