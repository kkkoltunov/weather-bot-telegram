using WeatherBot.Core.Enums;

namespace WeatherBot.Core.DTO;

public class WeatherInfo
{
    public DateTime DateUtc { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double Wind { get; set; }
    public WeatherType WeatherType { get; set; }
}