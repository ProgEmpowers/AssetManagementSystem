using AssetManagementSystem.Models;
using AuthService.Models.Dtos;
using AutoMapper;

namespace AuthService.Mappings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
