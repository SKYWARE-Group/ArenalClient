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
    [Display(GroupName = nameof(L10n.Attachment.Attachment.AttachmentGroupName),
        ShortName = nameof(L10n.Attachment.Attachment.AttachmentShortName),
        Name = nameof(L10n.Attachment.Attachment.AttachmentName),
        Description = nameof(L10n.Attachment.Attachment.AttachmentDescription),
        ResourceType = typeof(L10n.Attachment.Attachment))]
    public string Name { get; set; }

    /// <summary>
    /// Note from the placer.
    /// </summary>
    [Display(GroupName = nameof(L10n.Attachment.Attachment.NoteGroupName),
        ShortName = nameof(L10n.Attachment.Attachment.NoteShortName),
        Name = nameof(L10n.Attachment.Attachment.NoteName),
        Description = nameof(L10n.Attachment.Attachment.NoteDescription),
        Prompt = nameof(L10n.Attachment.Attachment.NotePrompt),
        ResourceType = typeof(L10n.Attachment.Attachment))]
    public Note Note { get; set; }

    /// <summary>
    /// Base64 encoded file content.
    /// </summary>
    public byte[] Data { get; set; }

}
