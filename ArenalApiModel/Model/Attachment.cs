using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Display(GroupName = nameof(L10n.Attachment.AttachmentGroupName),
        ShortName = nameof(L10n.Attachment.AttachmentShortName),
        Name = nameof(L10n.Attachment.AttachmentName),
        Description = nameof(L10n.Attachment.AttachmentDescription),
        ResourceType = typeof(L10n.Attachment))]
    public string Name { get; set; }

    /// <summary>
    /// Note from the placer.
    /// </summary>
    [Display(GroupName = nameof(L10n.Attachment.NoteGroupName),
        ShortName = nameof(L10n.Attachment.NoteShortName),
        Name = nameof(L10n.Attachment.NoteName),
        Description = nameof(L10n.Attachment.NoteDescription),
        Prompt = nameof(L10n.Attachment.NotePrompt),
        ResourceType = typeof(L10n.Attachment))]
    public Note Note { get; set; }

    /// <summary>
    /// Base64 encoded file content.
    /// </summary>
    public byte[] Data { get; set; }

}
