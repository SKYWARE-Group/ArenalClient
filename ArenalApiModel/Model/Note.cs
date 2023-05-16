using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represents a free text field.
/// </summary>
public class Note
{

    /// <summary>
    /// Formating type. See <seealso cref="NoteTypes"/>.
    /// </summary>
    [Display(GroupName = "Contact", ShortName = "Type", Name = "Type of the provided contact",
        Description = $"Enumerated types of different note types like plain text, rtf, html etc.",
        Prompt = "Please, select contact type from the menu.")]
    public string Type { get; set; } = NoteTypes.PLAIN_TEXT;

    /// <summary>
    /// Value of the note.
    /// </summary>
    [Display(GroupName = "Note", ShortName = "Content", Name = "Note Content",
        Description = $"Content of the note.",
        Prompt = "Please, enter note text to be placed.")]
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
