namespace Skyware.Arenal.Model.DocumentGeneration.Bg;


/// <summary>
/// Nhif Laboratory referral (Бл. № 4 на МЗ/НЗОК)
/// </summary>
public class NhifLabReferral
{

    #region Referral Metadata

    /// <summary>
    /// The Bulgarian National Reference Number for the referral.
    /// </summary>
    public string Nrn { get; set; }

    /// <summary>
    /// The LRN number for the referral.
    /// </summary>
    public string Lrn { get; set; }

    #endregion

    #region Patient Data

    /// <summary>
    /// The patient that the referral is targeted to.
    /// </summary>
    public Patient Patient { get; set; }

    #endregion

    #region Issuer Doctor Data

    /// <summary>
    /// The referral issuer's information.
    /// </summary>
    public Doctor Issuer { get; set; }

    #endregion

    #region Laboratory Data

    #endregion

}
