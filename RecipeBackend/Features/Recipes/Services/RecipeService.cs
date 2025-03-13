using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Core.Filters;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Repositories;

namespace RecipeBackend.Features.Recipes.Services;

public class RecipeService(
    RecipeRepository repository,
    CategoryRepository categoryRepo,
    IMapper mapper,
    IWebHostEnvironment webEnv,
    IHttpContextAccessor httpContextAccessor)
    : ServiceBase("recipes", webEnv, httpContextAccessor)
{
    public async Task<Recipe> CreateRecipeAsync(RecipeCreateDto payload, int userId)
    {
        if (!await categoryRepo.CheckCategoryExistsAsync(id: payload.CategoryId))
        {
            throw new DoesNotExistException(
                $"{nameof(Category)} with {nameof(Category.Id)}: {payload.CategoryId} does not exist.");
        }

        var newRecipe = mapper.Map<Recipe>(payload);

        newRecipe.UserId = userId;


        return await repository.CreateRecipeAsync(newRecipe);
    }

    public async Task<RecipeListDto?> GetTrendingRecipeAsync()
    {
        var trendingRecipe = await repository.GetTrendingRecipeAsync();
        if (trendingRecipe?.Photo != null) trendingRecipe.Photo = $"{BaseUrl}/{trendingRecipe.Photo}";
        return trendingRecipe;
    }

    public async Task<List<RecipeListDto>> ListTrendingRecipesAsync(PaginationFilters? filters)
    {
        var trendingRecipes = await repository.ListTrendingRecipesAsync(filters);
        trendingRecipes.ForEach(r => r.Photo = r.Photo != null ? $"{BaseUrl}/{r.Photo}" : r.Photo);
        return trendingRecipes;
    }

    public async Task<List<RecipeListDto>> ListRecipesAsync(RecipeFilters filters)
    {
        var recipes = await repository.ListRecipesAsync(filters);
        recipes.ForEach(r => r.Photo = r.Photo != null ? $"{BaseUrl}/{r.Photo}" : r.Photo);

        var totalCount = await repository.GetRecipeCountAsync();
        Dictionary<string, dynamic> metadata = new Dictionary<string, dynamic>()
        {
            { "TotalCount", totalCount },
        };

        if (filters?.Limit != null)
        {
            metadata.Add("TotalPages", (int)Math.Ceiling((double)(totalCount / filters.Limit)!));
        }

        HttpContext.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

        return recipes;
    }
    
    
    public async Task<List<RecipeListCommunityDto>> ListCommunityRecipesAsync(RecipeFilters filters)
    {
        var recipes = await repository.ListCommunityRecipesAsync(filters);
        recipes.ForEach(r =>
        {
            r.Photo = r.Photo != null ? $"{BaseUrl}/{r.Photo}" : string.Empty;
            r.User.ProfilePhoto = r.User.ProfilePhoto != null ? $"{BaseUrl}/{r.User.ProfilePhoto}" : string.Empty;
        });

        var totalCount = await repository.GetRecipeCountAsync();
        var metadata = new Dictionary<string, dynamic>
        {
            { "TotalCount", totalCount }
        };

        if (filters.Limit != null)
        {
            metadata.Add("TotalPages", (int)Math.Ceiling((double)(totalCount / filters.Limit)!));
        }

        HttpContext.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

        return recipes;
    }
    
    public async Task<RecipeDetailReviewsDto> GetRecipeForReviews(int id)
    {
        var recipe = await repository.GetRecipeForReviews(id);
        return recipe;
    }



    public async Task<List<RecipeListDto>> ListMyRecipesAsync(int userId, PaginationFilters? filters)
    {
        var myRecipes = await repository.ListMyRecipesAsync(userId, filters);
        myRecipes.ForEach(r => r.Photo = r.Photo != null ? $"{BaseUrl}/{r.Photo}" : r.Photo);
        return myRecipes;
    }

    public async Task<RecipeDetailDto> GetRecipeAsync(int id)
    {
        var recipe = await repository.GetRecipeAsync(id);
        DoesNotExistException.ThrowIfNull(recipe, $"Recipe with id: {id} does not exist.");
        if (recipe.Photo != null)
            recipe.Photo = $"{BaseUrl}/{recipe.Photo}";

        if (recipe.User.ProfilePhoto != null)
            recipe.User.ProfilePhoto = $"{BaseUrl}/{recipe.User.ProfilePhoto}";
        
        if (recipe.VideoRecipe != null)
            recipe.VideoRecipe = $"{BaseUrl}/{recipe.VideoRecipe}";

        return recipe;
    }


    public async Task<Recipe> UpdateRecipeAsync(int id, RecipeUpdateDto payload)
    {
        var recipe = await repository.GetRecipeForUpdateAsync(id);
        DoesNotExistException.ThrowIfNull(recipe, $"{nameof(Recipe)} with {nameof(Recipe.Id)}: {id} does not exist.");

        if (recipe.Photo != null && payload.Photo != null)
        {
            DeleteUploadsFile(recipe.Photo);
        }

        if (recipe.VideoRecipe != null && payload.VideoRecipe != null)
        {
            DeleteUploadsFile(recipe.VideoRecipe);
        }


        mapper.Map(payload, recipe);
        if (payload.Photo != null)
            recipe.Photo = await SaveUploadsFileAsync(payload.Photo);
        if (payload.VideoRecipe != null)
            recipe.VideoRecipe = await SaveUploadsFileAsync(payload.VideoRecipe);
        await repository.UpdateRecipeAsync();
        return recipe;
    }
}