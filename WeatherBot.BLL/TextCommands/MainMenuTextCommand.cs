using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.BLL.Interfaces;
using WeatherBot.BLL.Keyboards.UserKeyboard;
using WeatherBot.Core.DTO;
using WeatherBot.Core.Enums;

namespace WeatherBot.BLL.TextCommands;

public class MainMenuTextCommand : ITextCommand
{
    public async Task Execute(ITelegramBotClient client, UserDto? user, Message message,
        ServiceContainer serviceContainer)
    {
        user!.State = State.Main;
        await serviceContainer.UserService.UpdateAsync(user);
        await client.SendTextMessageAsync(user.Id,
            "Привет. Отправь мне геолокацию и я скину тебе погоду.", replyMarkup: MainKeyboard.MainReplyKeyboard);
    }

    public bool Compare(Message message, UserDto? user)
    {
        return message.Text == "/start";
    }
}