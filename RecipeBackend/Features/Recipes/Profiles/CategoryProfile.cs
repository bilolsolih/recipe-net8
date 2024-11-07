using AutoMapper;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryCreateDto>();
        CreateMap<CategoryCreateDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Recipes, opt => opt.Ignore());

        CreateMap<Category, CategoryDetailDto>();
        CreateMap<CategoryDetailDto, Category>();
        
        CreateMap<Category, CategoryListDto>();
        CreateMap<CategoryListDto, Category>();
    }
}