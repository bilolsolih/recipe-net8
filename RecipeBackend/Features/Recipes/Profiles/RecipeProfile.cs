using AutoMapper;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Profiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeListDto>();
        CreateMap<Recipe, RecipeCreateReviewDto>();
        CreateMap<Recipe, RecipeDetailReviewsDto>()
            .ForMember(dest => dest.ReviewsCount, opt => opt.MapFrom(src => src.Reviews.Count));
        CreateMap<Recipe, RecipeListCommunityDto>()
            .ForMember(dest => dest.ReviewsCount, opt => opt.MapFrom(src => src.Reviews.Count));
        CreateMap<RecipeCreateDto, Recipe>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.Updated, opt => opt.Ignore())
            .ForMember(dest => dest.LikedUsers, opt => opt.Ignore())
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
            .ForMember(dest => dest.Instructions, opt => opt.MapFrom(src => src.Instructions));

        CreateMap<Recipe, RecipeDetailDto>();

        CreateMap<RecipeUpdateDto, Recipe>()
            .ForAllMembers(opts => opts.Condition(
                (src, dest, srcMember) =>
                    srcMember != null &&
                    !(srcMember is string str && string.IsNullOrWhiteSpace(str)) &&
                    !(srcMember.GetType().IsValueType &&
                      srcMember.Equals(Activator.CreateInstance(srcMember.GetType())))
            ));
        CreateMap<IngredientCreateDto, Ingredient>();

        CreateMap<Instruction, InstructionDto>();
        CreateMap<Ingredient, IngredientDto>();

        CreateMap<InstructionDto, Instruction>();
        CreateMap<IngredientDto, Ingredient>();

        CreateMap<User, UserForRecipeDetailDto>();
    }
}