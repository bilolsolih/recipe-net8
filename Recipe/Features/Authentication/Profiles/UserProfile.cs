using AutoMapper;
using Recipe.Features.Authentication.DTOs;
using Recipe.Features.Authentication.Models;

namespace Recipe.Features.Authentication.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserCreateDto>();

        CreateMap<UserCreateDto, User>()
            .ForMember(u => u.Id, opt => opt.Ignore());
    }
}