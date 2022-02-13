using Newtonsoft.Json;
using WeatherBot.Core.Enums;

namespace WeatherBot.BLL.Converters;

public class WeatherTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        return reader.Value!.ToString() switch
        {
            "clear"=>  WeatherType.Clear, 
            "pcloudy"=>  WeatherType.PartlyCloudy,
            "mcloudy"=>  WeatherType.MostlyCloudy,
            "cloudy"=>  WeatherType.Cloudy,
            "humid"=>  WeatherType.Humid, 
            "lightrain"=>   WeatherType.LightRain, 
            "oshower"=> WeatherType.OccasionalShower,
            "ishower"=>  WeatherType.IsolatedShower,
            "lightsnow"=>  WeatherType.LightSnow,
            "rain"=>   WeatherType.Rain, 
            "snow"=>   WeatherType.Snow, 
            "rainsnow"=>   WeatherType.RainSnow, 
            "ts,"=> WeatherType.Thunderstorm, 
            "tsrain"=> WeatherType.ThunderstormRain,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(WeatherType);
    }
}