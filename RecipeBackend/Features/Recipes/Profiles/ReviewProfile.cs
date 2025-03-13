using AutoMapper;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewCreateDto, Review>();
        CreateMap<Review, ReviewDetailDto>();
        CreateMap<User, UserInReviewsDto>();
    }
}