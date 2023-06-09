using System.ComponentModel.DataAnnotations;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represents address of a person or organization. 
/// </summary>
[DisplayColumn(nameof(City))]
public class Address
{

    /// <summary>
    /// Two-letter country code, according to ISO 3166-1 alpha 2.
    /// </summary>
    [Display(GroupName = nameof(L10n.Address.Address.AddressGroupName),
        ShortName = nameof(L10n.Address.Address.CountryCodeShortName),
        Name = nameof(L10n.Address.Address.CountryCodeName),
        Description = nameof(L10n.Address.Address.CountryCodeDescription),
        Prompt = nameof(L10n.Address.Address.CountryCodePrompt),
        ResourceType = typeof(L10n.Address.Address))]
    public string CountryCode { get; set; }

    /// <summary>
    /// State or Region.
    /// </summary>
    [Display(GroupName = nameof(L10n.Address.Address.AddressGroupName),
        ShortName = nameof(L10n.Address.Address.RegionShortName),
        Name = nameof(L10n.Address.Address.RegionName),
        Description = nameof(L10n.Address.Address.RegionDescription),
        Prompt = nameof(L10n.Address.Address.RegionPrompt),
        ResourceType = typeof(L10n.Address.Address))]
    public string Region { get; set; }

    /// <summary>
    /// Name of the city/town.
    /// </summary>
    [Display(GroupName = nameof(L10n.Address.Address.AddressGroupName),
        ShortName = nameof(L10n.Address.Address.CityShortName),
        Name = nameof(L10n.Address.Address.CityName),
        Prompt = nameof(L10n.Address.Address.CityPrompt),
        ResourceType = typeof(L10n.Address.Address))]
    public string City { get; set; }

    /// <summary>
    /// Postal code.
    /// </summary>
    [Display(GroupName = nameof(L10n.Address.Address.AddressGroupName),
        ShortName = nameof(L10n.Address.Address.PostCodeShortName),
        Name = nameof(L10n.Address.Address.PostCodeName),
        Description = nameof(L10n.Address.Address.PostCodeDescription),
        Prompt = nameof(L10n.Address.Address.PostCodePrompt),
        ResourceType = typeof(L10n.Address.Address))]
    public string PostCode { get; set; }

    /// <summary>
    /// Actual address, e.g. "10 Downing St".
    /// </summary>
    [Display(GroupName = nameof(L10n.Address.Address.AddressGroupName),
        ShortName = nameof(L10n.Address.Address.StreetAddressShortName),
        Name = nameof(L10n.Address.Address.StreetAddressName),
        Description = nameof(L10n.Address.Address.StreetAddressDescription),
        Prompt = nameof(L10n.Address.Address.StreetAddressPrompt),
        ResourceType = typeof(L10n.Address.Address))]
    public string StreetAddress { get; set; }

    /// <summary>
    /// Geographic location.
    /// </summary>
    [Display(GroupName = nameof(L10n.Address.Address.AddressGroupName),
        ShortName = nameof(L10n.Address.Address.StreetAddressPrompt),
        Name = nameof(L10n.Address.Address.CoordinatesName),
        ResourceType = typeof(L10n.Address.Address))]
    public GeoCoordinates Coordinates { get; set; }


}
