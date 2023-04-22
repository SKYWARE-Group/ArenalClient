namespace Skyware.Arenal.Model;


/// <summary>
/// Represent a diagnosis, implies dagger-asterisk system of a ICD.
/// </summary>
public class Diagnosis
{

    /// <summary>
    /// The primary (dagger) code of a diagnosis.
    /// </summary>
    public Identifier PrimaryCode { get; set; }

    /// <summary>
    /// The clarification/complication (asterisk) code.
    /// </summary>
    public Identifier OptionalCode { get; set; }

    /// <summary>
    /// The diagnosis name, according to local language.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Diagnosis () { }

    /// <summary>
    /// Instantiates ICD-10 coded diagnosis.
    /// </summary>
    /// <param name="priamryCode"></param>
    /// <param name="optionalCode"></param>
    /// <param name="name"></param>
    public Diagnosis(string priamryCode, string optionalCode = null, string name = null) : this()
    {
        if (!string.IsNullOrWhiteSpace(priamryCode)) PrimaryCode = new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, priamryCode);
        if (!string.IsNullOrWhiteSpace(optionalCode)) OptionalCode = new Identifier(Authorities.WHO, Dictionaries.WHO_Icd10, optionalCode);
        Name = name;
    }

}
