using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Meta-data for constant usage
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class AuthorityUsageAttribute : Attribute
{

    /// <summary>
    /// Single type usage
    /// </summary>
    /// <param name="target"></param>
    public AuthorityUsageAttribute(Type target)
    {
        AllowedUsage = new Type[] { target };
    }

    /// <summary>
    /// Multiple types usage
    /// </summary>
    /// <param name="allowedTypes"></param>
    public AuthorityUsageAttribute(Type[] allowedTypes)
    {
        AllowedUsage = allowedTypes;
    }

    /// <summary>
    /// Get allowed types
    /// </summary>
    public IEnumerable<Type> AllowedUsage { get; } = new Type[] { };

}
