namespace Skyware.Arenal.Forms.Bg;


/// <summary>
/// Doctor (Bulgarian Forms)
/// Лекар (НЗОК документи)
/// </summary>
public class Doctor
{

    /// <summary>
    /// Practice code of a doctor.
    /// РЦЗ код на ЛЗ на лекар.
    /// </summary>
    public string PracticeCode { get; set; }

    /// <summary>
    /// Practice additional NHIF code (Optional).
    /// НЗОК код на ЛЗ (при договор в РЗОК, различна от тази по регистрация, опционално).
    /// </summary>
    public string PracticeNhifCode { get; set; }

    /// <summary>
    /// UIN of a doctor.
    /// УИН на лекаря.
    /// </summary>
    public string Uin { get; set; }

    /// <summary>
    /// UIN of a hired or deputy doctor.
    /// УИН на заместващ или нает лекар (опционално, в случай на <see cref="Role"/> != 0).
    /// </summary>
    public string DeputyUin { get; set; }

    /// <summary>
    /// Speciality Code of a doctor.
    /// Код на специалност на лекар.
    /// </summary>
    public string SpecialityCode { get; set; }

    /// <summary>
    /// Role of a doctor.
    /// Роля на лекаря.
    /// 0 = Титуляр, 1 = Заместващ, 2 = Нает (по подразбиране 0, титуляр).
    /// </summary>
    public byte Role { get; set; } = 0;

}
