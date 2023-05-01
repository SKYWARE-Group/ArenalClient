using System;
using System.Linq;
using System.Reflection;

namespace Skyware.Arenal.Model;


/// <summary>
/// Helper methods.
/// </summary>
public class Helpers
{

    /// <summary>
    /// Get all string constants in the type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string[] GetAllStringConstants(Type type)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue())
            .ToArray();
    }

    /// <summary>
    /// Get constants from a class, applicable to given type usage (see <see cref="AuthorityUsageAttribute"/>)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="targetUsage"></param>
    /// <returns></returns>
    public static string[] GetStringConstants(Type type, Type targetUsage)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(
                fi => fi.IsLiteral && 
                !fi.IsInitOnly && 
                fi.FieldType == typeof(string) && 
                (((AuthorityUsageAttribute)fi.GetCustomAttribute(typeof(AuthorityUsageAttribute)))?.AllowedUsage?.Any(x => x == targetUsage) ?? false))
            .Select(f => (string)f.GetRawConstantValue())
            .ToArray();
    }

    public static string[] GetStringConstants(Type type, string targetUsage)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(
                fi => fi.IsLiteral &&
                !fi.IsInitOnly &&
                fi.FieldType == typeof(string) &&
                (((DictionaryUsageAttribute)fi.GetCustomAttribute(typeof(DictionaryUsageAttribute)))?.AllowedUsage?.Any(x => x == targetUsage) ?? false))
            .Select(f => (string)f.GetRawConstantValue())
            .ToArray();
    }


}
