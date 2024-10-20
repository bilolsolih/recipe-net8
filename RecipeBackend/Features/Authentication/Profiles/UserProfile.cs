using AutoMapper;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserCreateDto>();

        CreateMap<UserCreateDto, User>()
            .ForMember(u => u.Id, opt => opt.Ignore());
    }
}