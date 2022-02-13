using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherBot.Core.DTO;
using WeatherBot.Core.Enums;

namespace WeatherBot.BLL.Converters;

public class WeatherConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var weather = new WeatherInfo
        {
            DateUtc = jo.SelectToken("date")!.ToObject<DateTime>(serializer),
            WeatherType = jo.SelectToken("weather")!.ToObject<WeatherType>(serializer),
            MaxTemperature = jo.SelectToken("temp2m.max")!.ToObject<int>(),
            MinTemperature = jo.SelectToken("temp2m.min")!.ToObject<int>(),
            Wind = jo.SelectToken("wind10m_max")!.ToObject<int>()
        };
        return weather;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(WeatherInfo);
    }
}