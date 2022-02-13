using Newtonsoft.Json;
using RestSharp;
using WeatherBot.BLL.Converters;
using WeatherBot.Core.DTO;
using WeatherBot.Core.Services;

namespace WeatherBot.BLL.Services;

public class WeathersGetterService : IWeathersGetterService
{
    public async Task<List<WeatherInfo>> GetWeatherAsync(Position position)
    {
        var response = await MakeRequest(position);
        return ParseWeather(response);
    }

    private async Task<string> MakeRequest(Position position)
    {
        var client = new RestClient("https://www.7timer.info/bin/");
        var request =
            new RestRequest($"civillight.php?lon={position.Longitude:F6}&lat={position.Latitude:F6}&output=json");
        var response = await client.ExecuteAsync(request);
        return response.Content!;
    }

    private List<WeatherInfo> ParseWeather(string response)
    {
        return JsonConvert.DeserializeObject<List<WeatherInfo>>(response,
            new WeatherTypeConverter(), new DateTimeConverter(), new WeatherConverter(), new WeatherListConverter())!;
    }
}