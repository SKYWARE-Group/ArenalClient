using System;

namespace Skyware.Arenal.Model;


/// <summary>
/// Constant values for Authority field in <see cref="Identifier"/>
/// </summary>
public class Authorities
{

    /// <summary>
    /// Arenal specific authority
    /// </summary>
    public const string ARENAL = "arenal";

    #region Bulgaria

/// <summary>
    /// Bulgarian Medical Association (УИН)
    /// </summary>
    [AuthorityUsage(typeof(Doctor))]
    public const string BG_BMA = "bg.bma";

    /// <summary>
    /// Bulgarian National Identifiers Registry (ЕГН)
    /// </summary>
    [AuthorityUsage(typeof(Patient))]
    public const string BG_GRAO = "bg.grao";

    /// <summary>
    /// Bulgarian Ministry Of Internal (ЛНЧ)
    /// </summary>
    [AuthorityUsage(typeof(Patient))]
    public const string BG_MI = "bg.mi";

    /// <summary>
    /// Bulgarian Health Information System (НЗИС, разни)
    /// </summary>
    [AuthorityUsage(new Type[] { typeof(Service), typeof(Patient) })]
    public const string BG_HIS = "bg.his";

    /// <summary>
    /// Bulgarian National Health Insurance Fund (НЗОК, разни)
    /// </summary>
    [AuthorityUsage(new Type[] { typeof(Service) })]
    public const string BG_HIF = "bg.nhif";

    #endregion 

    /// <summary>
    /// LOINC
    /// </summary>
    [AuthorityUsage(typeof(Service))]
    public const string LOINC = "org.loinc";

    /// <summary>
    /// World Health Organization
    /// </summary>
    [AuthorityUsage(typeof(Diagnosis))]
    public const string WHO = "int.who";

    /// <summary>
    /// HL7
    /// </summary>
    [AuthorityUsage(new Type[] { typeof(SampleType), typeof(Problem) })]
    public const string HL7 = "org.hl7";

    /// <summary>
    /// Any local (custom) identifier, not mentioned in other authority.
    /// </summary>
    [AuthorityUsage(new Type[] { typeof(Service), typeof(Patient), typeof(Sample) })]
    public const string LOCAL = "local";

}
