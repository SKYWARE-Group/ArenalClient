using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model;

/// <summary>
/// Meta-data for constant usage
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DictionaryUsageAttribute : Attribute
{

    /// <summary>
    /// Single authority usage
    /// </summary>
    /// <param name="target"></param>
    public DictionaryUsageAttribute(string target)
    {
        AllowedUsage = new string[] { target };
    }

    /// <summary>
    /// Multiple authorities usage
    /// </summary>
    /// <param name="allowedTypes"></param>
    public DictionaryUsageAttribute(string[] allowedTypes)
    {
        AllowedUsage = allowedTypes;
    }

    /// <summary>
    /// Get allowed authorities
    /// </summary>
    public IEnumerable<string> AllowedUsage { get; } = new string[] { };

}
