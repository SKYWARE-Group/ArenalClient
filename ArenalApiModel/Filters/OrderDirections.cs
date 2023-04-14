namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Order directions in sorting.
    /// </summary>
    public enum OrderDirections : byte
    {

        /// <summary>
        /// Ascending
        /// </summary>
        [FilterKeyword("asc")]
        Ascending,

        /// <summary>
        /// Descending
        /// </summary>
        [FilterKeyword("desc")]
        Descending

    }

}
