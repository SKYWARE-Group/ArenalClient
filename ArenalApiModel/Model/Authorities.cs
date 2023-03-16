using System;
using System.Security;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Constant values for Authority field in <see cref="Identifier"/>
    /// </summary>
    public class Authorities
    {

        /// <summary>
        /// Arenal specific authority
        /// </summary>
        public const string ARENAL = "arenal";

        /// <summary>
        /// Bulgarian Medical Association (УИН)
        /// </summary>
        [ArenalUsage(typeof(Doctor))]
        public const string BG_BMA = "bg.bma";

        /// <summary>
        /// Bulgarian National Identifiers Registry (ЕГН)
        /// </summary>
        [ArenalUsage(typeof(Patient))]
        public const string BG_GRAO = "bg.grao";

        /// <summary>
        /// Bulgarian Ministry Of Internal (ЛНЧ)
        /// </summary>
        [ArenalUsage(typeof(Patient))]
        public const string BG_MI = "bg.mi";

        /// <summary>
        /// Bulgarian Health Information System (НЗИС, разни)
        /// </summary>
        [ArenalUsage(new Type[] { typeof(Service), typeof(Patient) })]
        public const string BG_HIS = "bg.his";

        /// <summary>
        /// LOINC
        /// </summary>
        [ArenalUsage(typeof(Service))]
        public const string LOINC = "org.loinc";

        /// <summary>
        /// World Health Organization
        /// </summary>
        [ArenalUsage(typeof(Diagnosis))]
        public const string WHO = "int.who";

        /// <summary>
        /// HL7
        /// </summary>
        [ArenalUsage(typeof(Sample))]
        public const string HL7 = "org.hl7";

        /// <summary>
        /// Any local (custom) identifier, not mentioned in other authority.
        /// </summary>
        [ArenalUsage(typeof(Service))]
        public const string LOCAL = "local";

    }


}
