using WeatherBot.Core.DTO;
using WeatherBot.Core.Interface;
using WeatherBot.Core.Repositories;
using WeatherBot.Core.Services;

namespace WeatherBot.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IOperationResult> AddAsync(UserDto user)
    {
        try
        {
            await _repository.AddAsync(user);
            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }

    public async Task<IOperationResult> UpdateAsync(UserDto user)
    {
        try
        {
            await _repository.UpdateAsync(user);
            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }

    public async Task<IOperationResult> DeleteAsync(UserDto user)
    {
        try
        {
            await _repository.DeleteAsync(user);
            return OperationResult.Ok();
        }
        catch (Exception ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }

    public Task<UserDto?> GetAsync(long id) => _repository.GetAsync(id);
}