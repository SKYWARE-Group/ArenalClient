using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Reference to a referral or order in an external system.
/// </summary>
public class LinkedReferral
{

    /// <summary>
    /// Identifier in external system.
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.LinkedReferral.IdentifierGroupName),
        ShortName = nameof(L10n.LinkedReferral.LinkedReferral.IdentifierShortName),
        Name = nameof(L10n.LinkedReferral.LinkedReferral.IdentifierName),
        Description = nameof(L10n.LinkedReferral.LinkedReferral.IdentifierDescription),
        ResourceType = typeof(L10n.LinkedReferral.LinkedReferral))]
    public LinkedReferral Identifier { get; set; }

    /// <summary>
    /// Date and time the referral will expire (UTC).
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.LinkedReferral.ExpirationGroupName),
        ShortName = nameof(L10n.LinkedReferral.LinkedReferral.ExpirationShortName),
        Name = nameof(L10n.LinkedReferral.LinkedReferral.ExpirationName),
        Description = nameof(L10n.LinkedReferral.LinkedReferral.ExpirationDescription),
        Prompt = nameof(L10n.LinkedReferral.LinkedReferral.ExpirationPrompt),
        ResourceType = typeof(L10n.LinkedReferral.LinkedReferral))]
    public DateTime? Expiration { get; set; }

    /// <summary>
    /// Date and time the referral will expire (Local date and time).
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.LinkedReferral.LocalExpirationGroupName),
        ShortName = nameof(L10n.LinkedReferral.LinkedReferral.LocalExpirationShortName),
        Name = nameof(L10n.LinkedReferral.LinkedReferral.LocalExpirationName),
        Description = nameof(L10n.LinkedReferral.LinkedReferral.LocalExpirationDescription),
        Prompt = nameof(L10n.LinkedReferral.LinkedReferral.LocalExpirationPrompt),
        ResourceType = typeof(L10n.LinkedReferral.LinkedReferral))]
    public DateTime? LocalExpiration { get => Expiration?.ToLocalTime(); }

    /// <summary>
    /// Note from the placer
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.LinkedReferral.NoteGroupName),
        ShortName = nameof(L10n.LinkedReferral.LinkedReferral.NoteShortName),
        Name = nameof(L10n.LinkedReferral.LinkedReferral.NoteName),
        Description = nameof(L10n.LinkedReferral.LinkedReferral.NoteDescription),
        Prompt = nameof(L10n.LinkedReferral.LinkedReferral.NotePrompt),
        ResourceType = typeof(L10n.LinkedReferral.LinkedReferral))]
    public Note Note { get; set; }

}
