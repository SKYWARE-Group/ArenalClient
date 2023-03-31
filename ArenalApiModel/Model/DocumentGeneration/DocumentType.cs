using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal.Model.DocumentGeneration
{

    /// <summary>
    /// Document (form) generation item.
    /// </summary>
    public class DocumentType
    {

        /// <summary>
        /// Identifier of a document (form).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the document (form).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of expected class.
        /// </summary>
        public string DataClass { get; set; }

    }

}
