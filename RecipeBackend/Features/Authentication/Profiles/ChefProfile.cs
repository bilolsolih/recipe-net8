using AutoMapper;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Profiles;

public class ChefProfile : Profile
{
    public ChefProfile()
    {
        CreateMap<User, ChefListDto>();
    }
}