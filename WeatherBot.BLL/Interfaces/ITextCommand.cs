using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Core.DTO;

namespace WeatherBot.BLL.Interfaces;

public interface ITextCommand
{
    public Task Execute(ITelegramBotClient client, UserDto? user, Message message, ServiceContainer serviceContainer);

    public bool Compare(Message message, UserDto? user);
}