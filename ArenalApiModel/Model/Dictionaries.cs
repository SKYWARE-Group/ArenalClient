using System;

namespace Skyware.Arenal.Model;


/// <summary>
/// Constant values for Dictionary field in <see cref="Identifier"/>
/// </summary>
public class Dictionaries
{

    #region HL7

    /// <summary>
    /// HL7 Body Parts (Table 0070)
    /// </summary>
    [DictionaryUsage(Authorities.HL7)]
    public const string HL7_0070_SampleType = "0070";

    /// <summary>
    /// HL7 Sample Types (Table 0487)
    /// </summary>
    [DictionaryUsage(Authorities.HL7)]
    public const string HL7_0487_SampleType = "0487";

    /// <summary>
    /// LOINC Sample Additive/Preservative (Table 0371)
    /// </summary>
    [DictionaryUsage(Authorities.HL7)]
    public const string HL7_0371_SampleAdditive = "0371";

    /// <summary>
    /// HL7 Sample Reject Reasons (Table 0490)
    /// </summary>
    [DictionaryUsage(Authorities.HL7)]
    public const string HL7_0490_SampleRejectReasons = "0490";

    #endregion

    #region WHO

    /// <summary>
    /// ICD-9
    /// </summary>
    [Obsolete]
    [DictionaryUsage(Authorities.WHO)]
    public const string WHO_Icd9 = "icd-9";

    /// <summary>
    /// ICD-10
    /// </summary>
    [DictionaryUsage(Authorities.WHO)]
    public const string WHO_Icd10 = "icd-10";

    /// <summary>
    /// ICD-11
    /// </summary>
    [DictionaryUsage(Authorities.WHO)]
    public const string WHO_Icd11 = "icd-11";

    #endregion

    #region Bulgarian NHIS

    /// <summary>
    /// Newborns in Bulgarian NHIS
    /// </summary>
    [DictionaryUsage(Authorities.BG_HIS)]
    public const string BG_NHIS_Newborn = "nborn";

    /// <summary>
    /// Examinations, according to Bulgarian NHIS
    /// </summary>
    [DictionaryUsage(Authorities.BG_HIS)]
    public const string BG_NHIS_CL022 = "cl022";

    /// <summary>
    /// Tests (result entries), according to Bulgarian NHIS
    /// </summary>
    [DictionaryUsage(Authorities.BG_HIS)]
    public const string BG_NHIS_CL024 = "cl024";

    #endregion

    #region Bulgarian NHIF

    /// <summary>
    /// Products in Bulgarian NHIF (examination)
    /// </summary>
    [DictionaryUsage(Authorities.BG_HIF)]
    public const string BG_NHIF_Product = "prod";

    /// <summary>
    /// Medical institutions in Bulgarian NHIF (НЗОК номер)
    /// </summary>
    [DictionaryUsage(Authorities.BG_HIF)]
    public const string BG_NHIF_MedicalInstitution = "org";

    /// <summary>
    /// Specialties in Bulgarian NHIF (Код на специалност)
    /// </summary>
    [DictionaryUsage(Authorities.BG_HIF)]
    public const string BG_NHIF_Specialty = "spec";


    #endregion

}
