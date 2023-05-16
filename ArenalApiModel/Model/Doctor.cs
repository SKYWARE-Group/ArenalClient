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
    [Display(GroupName = "Doctor", ShortName = "Title", Name = "Title",
        Description = $"Enumerated types of doctor titles, such as D-r, Prof. etc.",
        Prompt = "Please, select title from the menu.")]
    public string Title { get; set; }

}
