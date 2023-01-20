/// <summary>
/// Reference to a referral or order in an external system.
/// </summary>
public class LinkedReferral
{

    /// <summary>
    /// Identifier of an external system where the referral is stored, such as 'bg.his', etc.
    /// </summary>
    public string SystemId { get; set; }

    /// <summary>
    /// Identifier of the linked referral in the external system such as NRN, etc.
    /// </summary>
    public string Id { get; set; }

}

