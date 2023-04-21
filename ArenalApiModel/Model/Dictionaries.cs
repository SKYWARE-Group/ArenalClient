using System;

namespace Skyware.Arenal.Model;


/// <summary>
/// Constant values for Dictionary field in <see cref="Identifier"/>
/// </summary>
public class Dictionaries
{

    /// <summary>
    /// HL7 Sample Types (Table 0487)
    /// </summary>
    public const string HL7_0487_SampleType = "0487";

    /// <summary>
    /// LOINC Sample Additive/Preservative (Table 0371)
    /// </summary>
    public const string HL7_0487_SampleAdditive = "0371";

    /// <summary>
    /// ICD-9
    /// </summary>
    [Obsolete]
    public const string WHO_Icd9 = "icd-9";

    /// <summary>
    /// ICD-10
    /// </summary>
    public const string WHO_Icd10 = "icd-10";

    /// <summary>
    /// ICD-11
    /// </summary>
    public const string WHO_Icd11 = "icd-11";

}
