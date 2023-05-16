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
    [Display(GroupName = "Address", ShortName = "Code", Name = "CountryCode",
        Description = "Country code of the address country.",
        Prompt = "Please, enter a valid country code, according to ISO 3166-1 alpha 2.")]
    public string CountryCode { get; set; }

    /// <summary>
    /// State or Region.
    /// </summary>
    [Display(GroupName = "Address", ShortName = "Region", Name = "State/Region",
        Description = "Region in the country.",
        Prompt = "Please, enter country region.")]
    public string Region { get; set; }

    /// <summary>
    /// Name of the city/town.
    /// </summary>
    [Display(GroupName = "Address", ShortName = "City", Name = "City/Town",
        Description = "City",
        Prompt = "Please, enter city.")]
    public string City { get; set; }

    /// <summary>
    /// Postal code.
    /// </summary>
    [Display(GroupName = "Address", ShortName = "PostCode", Name = "Postal code",
        Description = "Post code of the address",
        Prompt = "Please, enter address post code.")]
    public string PostCode { get; set; }

    /// <summary>
    /// Actual address, e.g. "10 Downing St".
    /// </summary>
    [Display(GroupName = "Address", ShortName = "Street", Name = "Street address",
        Description = "Street",
        Prompt = "Please, enter street address.")]
    public string StreetAddress { get; set; }

    /// <summary>
    /// Geographic location.
    /// </summary>
    [Display(GroupName = "Coordinates", ShortName = "Coordinates", Name = "Geographic location")]
    public GeoCoordinates Coordinates { get; set; }


}
