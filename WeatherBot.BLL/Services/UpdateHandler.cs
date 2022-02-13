using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeatherBot.BLL.Interfaces;
using WeatherBot.BLL.TextCommands;
using WeatherBot.Core.Services;

namespace WeatherBot.BLL.Services;

public class UpdateHandler : IUpdateHandler<Update>
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<UpdateHandler> _logger;
    private readonly ServiceContainer _serviceContainer;

    public UpdateHandler(IUserService userService,
        IWeathersGetterService weathersGetterService, ITelegramBotClient botClient,
        ILogger<UpdateHandler> logger)
    {
        _botClient = botClient;
        _serviceContainer = new ServiceContainer(userService, weathersGetterService);
        _logger = logger;
    }

    private static readonly List<ITextCommand> TextCommands = new()
    {
        new StartTextCommand(),
        new MainMenuTextCommand(),
        new GetWeatherTextCommand()
    };

    private static readonly List<ICallbackQueryCommand> CallbackQueryCommands = new()
    {
    };

    public async Task HandleUpdateAsync(Update update)
    {
        var handler = update.Type switch
        {
            // UpdateType.Unknown:
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            UpdateType.Message => BotOnMessageReceived(update.Message!),
            UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery!),
            _ => UnknownUpdateHandlerAsync(update)
        };

        try
        {
            await handler;
        }
        catch (Exception exception)
        {
            HandleErrorAsync(update, exception);
        }
    }

    private void HandleErrorAsync(Update update, Exception ex)
    {
        _logger.LogError(ex, "Update id: {Id}", update.Id);
    }

    private Task UnknownUpdateHandlerAsync(Update update)
    {
        return Task.CompletedTask;
    }

    private async Task BotOnCallbackQueryReceived(CallbackQuery updateCallbackQuery)
    {
        var user = await _serviceContainer.UserService.GetAsync(updateCallbackQuery.From.Id);

        var command = CallbackQueryCommands.FirstOrDefault(command => command.Compare(updateCallbackQuery, user));
        if (command != null)
            await command.Execute(_botClient, user, updateCallbackQuery, _serviceContainer);
    }

    private async Task BotOnMessageReceived(Message updateMessage)
    {
        var user = await _serviceContainer.UserService.GetAsync(updateMessage.From!.Id);
        var command = TextCommands.FirstOrDefault(command => command.Compare(updateMessage, user));
        if (command != null)
            await command.Execute(_botClient, user, updateMessage, _serviceContainer);
    }
}