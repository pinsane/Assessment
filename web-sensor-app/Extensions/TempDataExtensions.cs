using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace web_sensor_app.Extensions;

public static class TempDataExtensions
{
    public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonSerializer.Serialize(value);
    }

    public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        tempData.TryGetValue(key, out var obj);
        return obj == null ? null : JsonSerializer.Deserialize<T>((string)obj);
    }
}
