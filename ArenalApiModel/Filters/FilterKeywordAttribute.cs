using System;

namespace Skyware.Arenal.Filters
{

    /// <summary>
    /// Custom attribute for defining Arenal filter language keywords.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class FilterKeywordAttribute : Attribute
    {

        /// <summary>
        /// Arenal filter keyword.
        /// </summary>
        public string Keyword { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="keyword"></param>
        public FilterKeywordAttribute(string keyword)
        {
            Keyword = keyword;
        }
    }

}
