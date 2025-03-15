using AutoMapper;
using NextHome.Application.DTOs;
using NextHome.Domain.Entities;

namespace NextHome.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
       
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
    }
}
