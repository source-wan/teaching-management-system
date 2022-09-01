using System.Reflection;

namespace EMS.App.Util;
public static class ObjectHelper
{
    public static bool HasNullOrEmpty(this object? obj, BindingFlags? flags) =>
        obj == null
        || obj.GetType().GetProperties(flags ?? BindingFlags.Default)
        .Any(prop => prop == null || prop.GetValue(obj) == null);

    public static bool IsNullOrEmpty(this object? obj) =>
        obj == null
        || obj.GetType().GetProperties()
        .All(prop => prop == null || prop.GetValue(obj) == null);

    public static bool IsNullOrEmpty(this PropertyInfo? prop) => prop == null;

    public static bool IsNullOrEmpty(this PropertyInfo? prop, object obj)
    {
        if (prop == null) return true;
        var value = prop.GetValue(obj);
        if (value == null || value.ToString().IsNullOrEmpty()) return true;

        return false;
    }
}
