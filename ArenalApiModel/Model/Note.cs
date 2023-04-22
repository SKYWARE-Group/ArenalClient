namespace Skyware.Arenal.Model;


/// <summary>
/// Represents a free text field.
/// </summary>
public class Note
{

    /// <summary>
    /// Formating type. See <seealso cref="NoteTypes"/>.
    /// </summary>
    public string Type { get; set; } = NoteTypes.PLAIN_TEXT;

    /// <summary>
    /// Value of the note.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Note() { }

    /// <summary>
    /// Instantiate object with a plain text note.
    /// </summary>
    /// <param name="note"></param>
    public Note(string note) : this()
    {
        Value = note;
    }

}
