using AutoMapper;
using Data.Models;
using Service.DTOs;

namespace Service.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}
