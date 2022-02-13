using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeatherBot.BLL.Interfaces;
using WeatherBot.Core.DTO;
using WeatherBot.Core.Enums;

namespace WeatherBot.BLL.TextCommands;

public class GetWeatherTextCommand : ITextCommand
{
    public async Task Execute(ITelegramBotClient client, UserDto? user, Message message,
        ServiceContainer serviceContainer)
    {
        List<WeatherInfo> weather;
        try
        {
            weather = await serviceContainer.WeathersGetterService.GetWeatherAsync(new Position
            {
                Latitude = message.Location!.Latitude,
                Longitude = message.Location.Longitude
            });
        }
        catch (Exception ex)
        {
            await client.SendTextMessageAsync(user!.Id, $"Ошибка: {ex.Message}. Повторите попытку позже.");
            return;
        }

        
        var data = string.Join("\n\n", weather.Select(info =>
            $"<b>Дата:</b> <code>{TimeZoneInfo.ConvertTimeFromUtc(info.DateUtc, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")):d}</code>\n<b>Максимальная температура:</b> <code>{info.MaxTemperature}</code> (°C)\n<b>Минимальная температура:</b> <code>{info.MinTemperature}</code> (°C)\n<b>Шкала ветра:</b> <code>{info.Wind}</code>\n<b>Погода:</b> <code>{GetWeatherType(info.WeatherType)}</code>"));
        await client.SendTextMessageAsync(user!.Id, data, ParseMode.Html);
    }

    public bool Compare(Message message, UserDto? user)
    {
        return message.Type == MessageType.Location && user!.State == State.Main;
    }

    private string GetWeatherType(WeatherType type)
    {
        return type switch
        {
            WeatherType.Clear => "Ясно",
            WeatherType.PartlyCloudy => "Переменная облачность",
            WeatherType.MostlyCloudy => "В основном облачно",
            WeatherType.Cloudy => "Облачно",
            WeatherType.Humid => "Влажно",
            WeatherType.LightRain => "Небольшой дождь",
            WeatherType.OccasionalShower => "Редкие ливни",
            WeatherType.IsolatedShower => "Изолированные ливни",
            WeatherType.LightSnow => "Легкий снег",
            WeatherType.Rain => "Дождь",
            WeatherType.Snow => "Снег",
            WeatherType.RainSnow => "Снег с дождем",
            WeatherType.Thunderstorm => "Гроза",
            WeatherType.ThunderstormRain => "Гроза с дождём",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}