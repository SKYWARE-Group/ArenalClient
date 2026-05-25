using System.Text.Json.Serialization;

namespace Skyware.Arenal.Forms.Bg;

/// <summary>
/// Examination, an item in a referral.
/// Изследване в НМДД.
/// </summary>
public class ReferralItem
{

    /// <summary>
    /// Code of the examination, according to NHIF.
    /// НЗОК код на изследването.
    /// </summary>
    public string NhifCode { get; set; }

    /// <summary>
    /// Statistics code of the examination.
    /// КСМП код на изследването.
    /// </summary>
    [JsonPropertyName("statCode")]
    public string StatisticsCode { get; set; }

    /// <summary>
    /// Flag for the status (performed or not).
    /// Флаг за изпълнено изследване.
    /// </summary>
    public bool Done { get; set; } = true;

    /// <summary>
    /// UIN code of the performing doctor.
    /// УИН на лекаря-изпълнител.
    /// </summary>
    public string Uin { get; set; }

    /// <summary>
    /// Speciality code of the performing doctor.
    /// Код на специалност на лекаря-изпълнител.
    /// </summary>
    [JsonPropertyName("specCode")]
    public string SpecialtyCode { get; set; }

}

