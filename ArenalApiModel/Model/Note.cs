namespace Skyware.Arenal.Model;


/// <summary>
/// Represents a free text field.
/// </summary>
public class Note
{

    /// <summary>
    /// Minimum allowed length of a note.
    /// </summary>
    public const int MIN_LEN = 2; 

    /// <summary>
    /// Maximum allowed length of a note.
    /// </summary>
    public const int MAX_LEN = 1024; //Worts case - 2Kb

    /// <summary>
    /// Formating type, amongst <seealso cref="NoteTypes"/>.
    /// </summary>
    public string Type { get; set; } = NoteTypes.PLAIN_TEXT;

    /// <summary>
    /// Value of the note. Length must be between <see cref="MIN_LEN"/> and <see cref="MAX_LEN"/>.
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
