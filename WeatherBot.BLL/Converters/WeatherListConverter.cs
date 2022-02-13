using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherBot.Core.DTO;

namespace WeatherBot.BLL.Converters;

public class WeatherListConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var x = jo["dataseries"]!;
        return x.Select(token => token.ToObject<WeatherInfo>(serializer)).ToList();
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<WeatherInfo>);
    }
}