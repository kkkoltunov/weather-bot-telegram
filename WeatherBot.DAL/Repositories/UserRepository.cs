using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WeatherBot.Core.DTO;
using WeatherBot.Core.Repositories;

namespace WeatherBot.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<UserDto?> GetAsync(long id)
    {
        return _dbContext.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task AddAsync(UserDto user) => UpdateAsync(user);

    public async Task UpdateAsync(UserDto user)
    {
        var userEntity = await _dbContext.Users.Persist(_mapper).InsertOrUpdateAsync(user);
        user.Id = userEntity.Id;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserDto user)
    {
        await _dbContext.Users.Persist(_mapper).RemoveAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}