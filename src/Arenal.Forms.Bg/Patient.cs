using System;
using System.Text.Json.Serialization;

namespace Skyware.Arenal.Forms.Bg;

/// <summary>
/// Patient (Bulgarian Forms).
/// Пациент (в документи по НЗОК).
/// </summary>
public class Patient
{

    /// <summary>
    /// National identifier of the patient.
    /// ЕГН или ЛНЧ на пациента.
    /// </summary>
    [JsonPropertyName("pin")]
    public string NationalIdentifier { get; set; }

    /// <summary>
    /// Patient identifier type.
    /// Вид на идентификатора на пациента.
    /// 1 = ЕГН, 2 = ЛНЧ, 3 = Друг (отпечатва се <see cref="Pid"/>).
    /// </summary>
    public byte PidType { get; set; } = 1; //ЕГН

    /// <summary>
    /// Patient identifier, when <see cref="PidType"/> is not 0 or 1.
    /// Идентификатор на пациента, когато е различен от ЕГН или ЛНЧ.
    /// </summary>
    public string Pid { get; set; } = string.Empty;

    /// <summary>
    /// Regional Health Insurance Fund code.
    /// Номер на РЗОК.
    /// </summary>
    public string Rhif { get; set; }

    /// <summary>
    /// Health region where the patient resides.
    /// Здравен регион на пациента.
    /// </summary>
    [JsonPropertyName("hRegion")]
    public string HealthRegion { get; set; }

    /// <summary>
    /// Date Of Birth of the patient.
    /// Дата на раждане на пациента.
    /// </summary>
    [JsonPropertyName("dob")]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Country code of the patient's residence.
    /// Код на държава на гражданство на пациента.
    /// </summary>
    public string Country { get; set; } = "BG";

    /// <summary>
    /// Given Name.
    /// Име.
    /// </summary>
    public string GivenName { get; set; }

    /// <summary>
    /// Middle Name.
    /// Презиме.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// Family Name (Surname).
    /// Фамилия.
    /// </summary>
    public string FamilyName { get; set; }

    /// <summary>
    /// Address of the patient.
    /// Адрес на пациента по местоживеене.
    /// </summary>
    public Address Address { get; set; }



}
