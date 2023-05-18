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
    [Display(GroupName = nameof(L10n.LinkedReferral.IdentifierGroupName),
        ShortName = nameof(L10n.LinkedReferral.IdentifierShortName),
        Name = nameof(L10n.LinkedReferral.IdentifierName),
        Description = nameof(L10n.LinkedReferral.IdentifierDescription),
        ResourceType = typeof(L10n.LinkedReferral))]
    public LinkedReferral Identifier { get; set; }

    /// <summary>
    /// Date and time the referral will expire (UTC).
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.ExpirationGroupName),
        ShortName = nameof(L10n.LinkedReferral.ExpirationShortName),
        Name = nameof(L10n.LinkedReferral.ExpirationName),
        Description = nameof(L10n.LinkedReferral.ExpirationDescription),
        Prompt = nameof(L10n.LinkedReferral.ExpirationPrompt),
        ResourceType = typeof(L10n.LinkedReferral))]
    public DateTime? Expiration { get; set; }

    /// <summary>
    /// Date and time the referral will expire (Local date and time).
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.LocalExpirationGroupName),
        ShortName = nameof(L10n.LinkedReferral.LocalExpirationShortName),
        Name = nameof(L10n.LinkedReferral.LocalExpirationName),
        Description = nameof(L10n.LinkedReferral.LocalExpirationDescription),
        Prompt = nameof(L10n.LinkedReferral.LocalExpirationPrompt),
        ResourceType = typeof(L10n.LinkedReferral))]
    public DateTime? LocalExpiration { get => Expiration?.ToLocalTime(); }

    /// <summary>
    /// Note from the placer
    /// </summary>
    [Display(GroupName = nameof(L10n.LinkedReferral.NoteGroupName),
        ShortName = nameof(L10n.LinkedReferral.NoteShortName),
        Name = nameof(L10n.LinkedReferral.NoteName),
        Description = nameof(L10n.LinkedReferral.NoteDescription),
        Prompt = nameof(L10n.LinkedReferral.NotePrompt),
        ResourceType = typeof(L10n.LinkedReferral))]
    public Note Note { get; set; }

}
