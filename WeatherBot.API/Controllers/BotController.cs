using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WeatherBot.Core.Services;

namespace WeatherBot.API.Controllers;

public class BotController : ControllerBase
{
    private readonly IUpdateHandler<Update> _handler;

    public BotController(IUpdateHandler<Update> handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        await _handler.HandleUpdateAsync(update);
        return Ok();
    }
}