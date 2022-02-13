using WeatherBot.Core.DTO;

namespace WeatherBot.Core.Repositories;

public interface IUserRepository
{
    Task<UserDto?> GetAsync(long id);
    Task AddAsync(UserDto user);
    Task UpdateAsync(UserDto user);
    Task DeleteAsync(UserDto user);
}