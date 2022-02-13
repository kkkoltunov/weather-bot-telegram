using WeatherBot.Core.DTO;
using WeatherBot.DAL.Entities;

namespace WeatherBot.DAL.Profile;

public class MapperProfile : AutoMapper.Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}