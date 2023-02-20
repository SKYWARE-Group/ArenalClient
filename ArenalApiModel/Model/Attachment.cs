using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Skyware.Arenal.Model
{

    /// <summary>
    /// A binary file. 
    /// </summary>
    public class Attachment
    {

        /// <summary>
        /// Identifier of a attachment.
        /// </summary>
        public Identifier Identifier { get; set; }

        /// <summary>
        /// File name, such as 'Patient234.pdf'.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base64 encoded file content.
        /// </summary>
        public byte[] Data { get; set; }

    }

}
