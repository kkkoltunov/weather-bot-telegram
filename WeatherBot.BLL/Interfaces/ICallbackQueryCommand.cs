using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Core.DTO;

namespace WeatherBot.BLL.Interfaces;

public interface ICallbackQueryCommand
{
    public Task Execute(ITelegramBotClient client, UserDto? user, CallbackQuery query, ServiceContainer serviceContainer);

    public bool Compare(CallbackQuery query, UserDto? user);
}