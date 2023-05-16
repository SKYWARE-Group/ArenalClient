using System.ComponentModel.DataAnnotations;

namespace Skyware.Arenal.Model;


/// <summary>
/// A binary file. 
/// </summary>
[DisplayColumn(nameof(Name))]
public class Attachment
{

    /// <summary>
    /// Identifier of a attachment.
    /// </summary>
    public Identifier Identifier { get; set; }

    /// <summary>
    /// File name, such as 'Patient234.pdf'.
    /// </summary>
    [Display(GroupName = "Attachment", ShortName = "File", Name = "File name")]
    public string Name { get; set; }

    /// <summary>
    /// Note from the placer.
    /// </summary>
    [Display(GroupName = "Note", ShortName = "Note", Name = "Note",
        Description = "Note from the uploader of the file.")]
    public Note Note { get; set; }

    /// <summary>
    /// Base64 encoded file content.
    /// </summary>
    public byte[] Data { get; set; }

}
