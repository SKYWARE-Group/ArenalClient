using System.Text.Json.Serialization;

namespace Skyware.Arenal.Forms.Bg;

/// <summary>
/// Residential address (Bulgarian forms)
/// Адрес (документи по НЗОК)
/// </summary>
public class Address
{

    /// <summary>
    /// City/Town.
    /// Град/село.
    /// </summary>
    public string Town { get; set; }

    /// <summary>
    /// Street name.
    /// Улица.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Street number.
    /// Номер (на къщата/блока).
    /// </summary>
    [JsonPropertyName("streetNum")]
    public string StreetNumber { get; set; }

    /// <summary>
    /// Residential area.
    /// Жилищен комплекс (ж.к.).
    /// </summary>
    public string Area { get; set; }

    /// <summary>
    /// Apartment block number.
    /// Номер на блок.
    /// </summary>
    public string Block { get; set; }

    /// <summary>
    /// Number or letter of the entrance.
    /// Номер/буква на вход (в блок).
    /// </summary>
    public string Entrance { get; set; }    

    /// <summary>
    /// Floor.
    /// Етаж.
    /// </summary>
    public string Floor { get; set; }

    /// <summary>
    /// Number of an apartment.
    /// Номер на апартамент.
    /// </summary>
    public string Apartment { get; set; }

}
