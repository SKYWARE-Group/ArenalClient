﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Skyware.Arenal.L10n {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Contact {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Contact() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Skyware.Arenal.L10n.Contact", typeof(Contact).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Contact.
        /// </summary>
        internal static string ContactGroupName {
            get {
                return ResourceManager.GetString("ContactGroupName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type of contacts like email or phone etc..
        /// </summary>
        internal static string ContactTypeDescription {
            get {
                return ResourceManager.GetString("ContactTypeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type of the provided contact.
        /// </summary>
        internal static string ContactTypeName {
            get {
                return ResourceManager.GetString("ContactTypeName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please, select contact type..
        /// </summary>
        internal static string ContactTypePrompt {
            get {
                return ResourceManager.GetString("ContactTypePrompt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type.
        /// </summary>
        internal static string ContactTypeShortName {
            get {
                return ResourceManager.GetString("ContactTypeShortName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value of the contact according to the selected type. .
        /// </summary>
        internal static string ContactValueDescription {
            get {
                return ResourceManager.GetString("ContactValueDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value.
        /// </summary>
        internal static string ContactValueName {
            get {
                return ResourceManager.GetString("ContactValueName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please, add value of the contact..
        /// </summary>
        internal static string ContactValuePrompt {
            get {
                return ResourceManager.GetString("ContactValuePrompt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value.
        /// </summary>
        internal static string ContactValueShortName {
            get {
                return ResourceManager.GetString("ContactValueShortName", resourceCulture);
            }
        }
    }
}
