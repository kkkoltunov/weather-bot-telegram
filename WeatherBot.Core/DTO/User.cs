using WeatherBot.Core.Enums;

namespace WeatherBot.Core.DTO;

public class UserDto
{
    public long Id { get; set; }
    public State State { get; set; } = State.Main;
}