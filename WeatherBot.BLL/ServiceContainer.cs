using WeatherBot.Core.Services;

namespace WeatherBot.BLL;

public class ServiceContainer
{
    public ServiceContainer(IUserService userService, IWeathersGetterService weathersGetterService)
    {
        UserService = userService;
        WeathersGetterService = weathersGetterService;
    }

    public IUserService UserService { get; set; }
    public IWeathersGetterService WeathersGetterService { get; set; }
}