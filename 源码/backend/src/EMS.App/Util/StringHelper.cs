namespace EMS.App.Util;
public static class StringHelper
{
    public static bool IsNullOrEmpty(this string? str) =>
        str == null
        || (!str.Any())
        || (!str.Replace(" ", "").Any());
}