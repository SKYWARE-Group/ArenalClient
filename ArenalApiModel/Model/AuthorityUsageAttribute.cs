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
    /// <param name="singleOccurrence"></param>
    /// <param name="exclusiveGroup"></param>
    public AuthorityUsageAttribute(Type target, bool singleOccurrence = false, string exclusiveGroup = null)
    {
        AllowedUsage = new Type[] { target };
        SingleOccurrence = singleOccurrence;
    }

    /// <summary>
    /// Multiple types usage
    /// </summary>
    /// <param name="allowedTypes"></param>
    /// <param name="singleOccurrence"></param>
    /// <param name="exclusiveGroup"></param>
    public AuthorityUsageAttribute(Type[] allowedTypes, bool singleOccurrence = false, string exclusiveGroup = null)
    {
        AllowedUsage = allowedTypes;
        SingleOccurrence = singleOccurrence;
    }

    /// <summary>
    /// Get allowed types
    /// </summary>
    public IEnumerable<Type> AllowedUsage { get; } = new Type[] { };

    /// <summary>
    /// If true, only one identifier withs authority is allowed for given object
    /// </summary>
    public bool SingleOccurrence { get; } = false;

    /// <summary>
    /// If set, <see cref="Identifier"/> in this group must appear only once within a list.
    /// </summary>
    public string ExclusiveGroup { get; }

}
