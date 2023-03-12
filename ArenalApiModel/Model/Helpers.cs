using System;
using System.Linq;
using System.Reflection;

namespace Skyware.Arenal.Model
{

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
        public static string[] GetAllConstants(Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(f => (string)f.GetRawConstantValue())
                .ToArray();
        }
    }

}
