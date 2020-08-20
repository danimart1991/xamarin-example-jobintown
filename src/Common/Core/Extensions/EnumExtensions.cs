using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Attributes;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var attribute = GetFirstOrDefaultAttribute<DisplayNameAttribute>(value);
            return attribute != null ? attribute.DisplayName : string.Empty;
        }

        private static T GetFirstOrDefaultAttribute<T>(Enum value)
            where T : Attribute
        {
            var attributes = GetAttributes<T>(value);
            return attributes.FirstOrDefault() as T;
        }

        private static IEnumerable<T> GetAttributes<T>(Enum value)
            where T : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetRuntimeField(name).GetCustomAttributes<T>(false);
        }
    }
}
