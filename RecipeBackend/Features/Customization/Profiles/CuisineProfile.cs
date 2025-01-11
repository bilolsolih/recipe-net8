using AutoMapper;
using RecipeBackend.Features.Customization.DTOs;
using RecipeBackend.Features.Customization.Models;

namespace RecipeBackend.Features.Customization.Profiles;

public class CuisineProfile : Profile
{
    public CuisineProfile()
    {
        CreateMap<CuisineCreateDto, Cuisine>();
        CreateMap<Cuisine, CuisineListDto>();
    }
    
}