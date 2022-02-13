using WeatherBot.Core.DTO;

namespace WeatherBot.Core.Services;

public interface IWeathersGetterService
{
    Task<List<WeatherInfo>> GetWeatherAsync(Position position);
}