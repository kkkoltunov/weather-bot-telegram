using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.BLL.Interfaces;
using WeatherBot.BLL.Keyboards.UserKeyboard;
using WeatherBot.Core.DTO;

namespace WeatherBot.BLL.TextCommands;

public class StartTextCommand : ITextCommand
{
    public async Task Execute(ITelegramBotClient client, UserDto? user, Message message,
        ServiceContainer serviceContainer)
    {
        user = new UserDto
        {
            Id = message.From!.Id
        };
        var result = await serviceContainer.UserService.AddAsync(user);

        if (result.Success)
        {
            await client.SendTextMessageAsync(user.Id,
                "Привет. Отправь мне геолокацию и я скину тебе погоду.", replyMarkup: MainKeyboard.MainReplyKeyboard);
            return;
        }

        await client.SendTextMessageAsync(user.Id,
            $"Ошибка: {result.ErrorMessage}. Попробуй написать сообщение еще раз.");
    }

    public bool Compare(Message message, UserDto? user)
    {
        return user == null;
    }
}