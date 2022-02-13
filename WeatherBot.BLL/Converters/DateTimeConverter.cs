using System.Globalization;
using Newtonsoft.Json;

namespace WeatherBot.BLL.Converters;

public class DateTimeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var date = reader.Value?.ToString();
        return DateTime.ParseExact(date!, "yyyyMMdd", CultureInfo.InvariantCulture).Add(DateTime.UtcNow.TimeOfDay);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime);
    }
}