namespace Skyware.Arenal.Tracking;

public class EntityChange
{

    /// <summary>
    /// Type of change
    /// </summary>
    public enum ChangeTypes : byte
    {
        /// <summary>
        /// Value types - changed
        /// </summary>
        Changed = 0,

        /// <summary>
        /// Collections - item added
        /// </summary>
        Added = 1,

        /// <summary>
        /// Collections - item deleted
        /// </summary>
        Deleted = 2,
    }


    public string PropertyName { get; set; }

    public ChangeTypes ChangeType { get; set; } = ChangeTypes.Changed;

    public object OldValue { get; set; }    

    public  object NewValue { get; set; }

    public override string ToString()
    {
        return $"{PropertyName}: {OldValue ?? "null"} -> {NewValue ?? "null"}";
    }

}
