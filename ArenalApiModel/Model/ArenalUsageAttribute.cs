using System;
using System.Collections.Generic;

namespace Skyware.Arenal.Model;


/// <summary>
/// Meta-data for constant usage
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ArenalUsageAttribute : Attribute
{

    private IEnumerable<Type> _AllowedTypes = new Type[] { };

    /// <summary>
    /// Single type usage
    /// </summary>
    /// <param name="target"></param>
    public ArenalUsageAttribute(Type target)
    {
        _AllowedTypes = new Type[] { target };
    }

    /// <summary>
    /// Multiple types usage
    /// </summary>
    /// <param name="allowedTypes"></param>
    public ArenalUsageAttribute(Type[] allowedTypes)
    {
        _AllowedTypes = allowedTypes;
    }

    /// <summary>
    /// Get allowed types
    /// </summary>
    public IEnumerable<Type> AllowedUsage { get => _AllowedTypes; }

}
