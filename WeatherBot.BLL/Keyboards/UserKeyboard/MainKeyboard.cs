using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.BLL.Keyboards.UserKeyboard;

public static class MainKeyboard
{
    public static readonly ReplyKeyboardMarkup MainReplyKeyboard =
        new(KeyboardButton.WithRequestLocation("Отправить локацию"))
        {
            ResizeKeyboard = true,
            InputFieldPlaceholder = "Нажмите на нужную кнопку"
        };


    public static InlineKeyboardMarkup Back(string query) =>
        new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("🔙 Назад", $"back_{query}"));

    public static readonly InlineKeyboardMarkup Main =
        new(InlineKeyboardButton.WithCallbackData("⭐ В главное меню", "mainMenu"));
}