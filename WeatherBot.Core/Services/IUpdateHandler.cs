namespace WeatherBot.Core.Services;

public interface IUpdateHandler<in T>
{
    Task HandleUpdateAsync(T update);
}