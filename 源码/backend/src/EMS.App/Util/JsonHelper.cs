using System.Text.Encodings.Web;
using System.Text.Json;

namespace EMS.App.Util
{
    public static class JsonHelper
    {
        public static string SerializeObject(this object? obj)
        {
            var options =
                new JsonSerializerOptions {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy=JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}