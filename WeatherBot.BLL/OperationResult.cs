using WeatherBot.Core.Interface;

namespace WeatherBot.BLL;

public class OperationResult : IOperationResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    private OperationResult(bool succeeded, string? errorMessage)
    {
        Success = succeeded;
        ErrorMessage = errorMessage;
    }

    public static OperationResult Ok()
    {
        return new OperationResult(true, null);
    }

    public static OperationResult Fail(string message)
    {
        return new OperationResult(false, message);
    }
}