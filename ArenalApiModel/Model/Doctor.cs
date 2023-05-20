using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Skyware.Arenal.Model;


/// <summary>
/// Represent a doctor.
/// </summary>
public class Doctor : PersonBase
{

    /// <summary>
    /// Title, such as D-r, Prof., etc.
    /// </summary>
    [Display(GroupName = nameof(L10n.Doctor.Doctor.DocotrGroupName),
        ShortName = nameof(L10n.Doctor.Doctor.DoctorShortName),
        Name = nameof(L10n.Doctor.Doctor.DoctorName),
        Description = nameof(L10n.Doctor.Doctor.DoctorDescription),
        Prompt = nameof(L10n.Doctor.Doctor.DoctorPrompt),
        ResourceType = typeof(L10n.Doctor.Doctor))]
    public string Title { get; set; }

}
