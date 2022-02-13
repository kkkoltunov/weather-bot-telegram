namespace WeatherBot.Core.Interface;

public interface IOperationResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}