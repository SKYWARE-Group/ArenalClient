namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Constant values for Authority field in <see cref="Identifier"/>
    /// </summary>
    public class Authorities
    {

        /// <summary>
        /// Bulgarian National Identifiers Registry (ЕГН)
        /// </summary>
        public const string BG_GRAO = "bg.grao";

        /// <summary>
        /// Bulgarian Health Information System (НЗИС)
        /// </summary>
        public const string BG_HIS = "bg.his";

        /// <summary>
        /// LOINC
        /// </summary>
        public const string LOINC = "org.loinc";

        /// <summary>
        /// H&7 tables
        /// </summary>
        public const string HL7 = "org.hl7";

        /// <summary>
        /// Any local (custom) identifier, not mentioned in other authority.
        /// </summary>
        public const string LOCAL = "local";

    }


}
