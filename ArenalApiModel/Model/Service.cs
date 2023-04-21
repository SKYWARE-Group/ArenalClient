using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Service (examination).
/// </summary>
public class Service
{

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Service() { }

    /// <summary>
    /// Instantiates a Service for laboratory examination, coded with Loinc.
    /// </summary>
    public Service(string loincCode, string name = null, string note = null)
    {
        if (!string.IsNullOrWhiteSpace(loincCode)) ServiceId = new Identifier(Authorities.LOINC, null, loincCode);
        Name = name;
        if (!string.IsNullOrWhiteSpace(note)) Note = new Note(note);
    }

    /// <summary>
    /// Identifier of a ordered examination or service.
    /// </summary>
    public Identifier ServiceId { get; set; }

    /// <summary>
    /// Additional identifiers, not defined in <see cref="ServiceId"/>.
    /// </summary>
    public IEnumerable<Identifier> AlternateIdentifiers { get; set; }

    /// <summary>
    /// Name of the service, according to the placer (optional).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Notes from the placer.
    /// </summary>
    public Note Note { get; set; }

    /// <summary>
    /// Ordering value, according to the provider's sorting.
    /// </summary>
    public int? Rank { get; set; }


}

