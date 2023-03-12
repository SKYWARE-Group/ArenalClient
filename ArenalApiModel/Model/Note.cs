namespace Skyware.Arenal.Model
{

    /// <summary>
    /// Represents a free text field.
    /// </summary>
    public class Note
    {

        /// <summary>
        /// Format type
        /// </summary>
        public string Type { get; set; } = NoteTypes.PLAIN_TEXT;

        /// <summary>
        /// Value of the note
        /// </summary>
        public string Value { get; set; }

    }

}
