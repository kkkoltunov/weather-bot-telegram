using WeatherBot.Core.DTO;
using WeatherBot.Core.Interface;

namespace WeatherBot.Core.Services;

public interface IUserService
{
    Task<IOperationResult> AddAsync(UserDto user);
    Task<IOperationResult> UpdateAsync(UserDto user);
    Task<IOperationResult> DeleteAsync(UserDto user);
    Task<UserDto?> GetAsync(long id);
}