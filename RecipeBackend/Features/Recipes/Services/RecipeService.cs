using AutoMapper;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
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
    IHttpContextAccessor httpContext)
    : ServiceBase("recipes", webEnv)
{
    public async Task<Recipe> CreateRecipeAsync(RecipeCreateDto payload, int userId)
    {
        if (!await categoryRepo.CheckCategoryExistsAsync(id: payload.CategoryId))
        {
            throw new DoesNotExistException($"{nameof(Category)} with {nameof(Category.Id)}: {payload.CategoryId} does not exist.");
        }

        var newRecipe = mapper.Map<Recipe>(payload);

        newRecipe.UserId = userId;

        return await repository.CreateRecipeAsync(newRecipe);
    }

    public async Task<List<RecipeListDto>> ListRecipesAsync(RecipeFilters? filters = null)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(RecipeService)}");
        var recipes = await repository.ListRecipesAsync(filters);
        var baseUrl = httpContext.HttpContext.GetUploadsBaseUrl() + '/';
        foreach (var recipe in recipes)
        {
            if (recipe.Photo != null)
                recipe.Photo = baseUrl + recipe.Photo;
        }
        return recipes;
    }

    public async Task<RecipeDetailDto> GetRecipeAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(RecipeService)}");
        var recipe = await repository.GetRecipeAsync(id);
        DoesNotExistException.ThrowIfNull(recipe, $"Recipe with id: {id} does not exist.");
        if (recipe.Photo != null)
            recipe.Photo = httpContext.HttpContext.GetUploadsBaseUrl() + '/' + recipe.Photo;
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