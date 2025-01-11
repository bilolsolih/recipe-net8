using AutoMapper;
using RecipeBackend.Features.Customization.DTOs;
using RecipeBackend.Features.Customization.Models;

namespace RecipeBackend.Features.Customization.Profiles;

public class AllergicIngredientProfile : Profile
{
    public AllergicIngredientProfile()
    {
        CreateMap<AllergicIngredientCreateDto, AllergicIngredient>();
        CreateMap<AllergicIngredient, AllergicIngredientListDto>();
    }
    
}