using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        var recipe = await repository.GetRecipeAsync(id);
        DoesNotExistException.ThrowIfNull(recipe, $"Recipe with id: {id} does not exist.");
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

    private async Task CheckRecipeExistsAsync(int id)
    {
        var exists = await repository.CheckRecipeExistsAsync(id);
        DoesNotExistException.ThrowIfNot(exists, $"{nameof(Recipe)} with {nameof(Recipe.Id)}: {id} does not exist.");
    }

    private async Task<string> HandleRecipeVideoAsync(IFormFile video)
    {
        var fileExtension = GetFileExtension(video);

        var lastPeriodIndex = video.FileName.LastIndexOf('.');
        var fileName = video.FileName[..lastPeriodIndex] + GenerateShortGuid();
        fileName += fileExtension;

        var filePath = Path.Combine(webEnv.GetUploadBasePath(), "recipes", fileName);

        if (!Directory.Exists(webEnv.GetUploadBasePath()))
        {
            Directory.CreateDirectory(webEnv.GetUploadBasePath());
        }

        if (!Directory.Exists(Path.Combine(webEnv.GetUploadBasePath(), "recipes")))
        {
            Directory.CreateDirectory(Path.Combine(webEnv.GetUploadBasePath(), "recipes"));
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await video.CopyToAsync(fileStream);

        var relativeFilePath = "recipes" + '/' + fileName;

        return relativeFilePath;
    }

    private string GetFileExtension(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        InvalidFileException.ThrowIfNull(fileExtension, $"Uploaded file {file.FileName} does not have an extension.");
        return fileExtension;
    }

    private string GenerateShortGuid(int length = 8)
    {
        var guid = Guid.NewGuid().ToString("N");
        return guid[..length];
    }
}