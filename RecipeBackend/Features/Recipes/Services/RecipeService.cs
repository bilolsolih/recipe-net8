using AutoMapper;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Repositories;

namespace RecipeBackend.Features.Recipes.Services;

public class RecipeService(RecipeRepository repository, CategoryRepository categoryRepo, IMapper mapper, IWebHostEnvironment webEnv)
{
    public async Task<Recipe> CreateRecipeAsync(RecipeCreateDto payload, int userId)
    {
        if (!await categoryRepo.CheckCategoryExistsAsync(id: payload.CategoryId))
        {
            throw new DoesNotExistException($"{nameof(Category)} with {nameof(Category.Id)}: {payload.CategoryId} does not exist.");
        }

        var newRecipe = mapper.Map<Recipe>(payload);

        var videoRecipe = await HandleRecipeVideoAsync(payload.VideoRecipe);

        newRecipe.VideoRecipe = videoRecipe;
        newRecipe.UserId = userId;

        // newRecipe.Ingredients = payload.Ingredients.Select(ingredientDto =>
        // {
        //     var ingredient = mapper.Map<Ingredient>(ingredientDto);
        //     ingredient.RecipeId = newRecipe.Id;
        //     return ingredient;
        // }).ToList();
        //
        // newRecipe.Instructions = payload.Instructions.Select(dto =>
        // {
        //     var instruction = mapper.Map<Instruction>(dto);
        //     instruction.RecipeId = newRecipe.Id;
        //     return instruction;
        // }).ToList();

        return await repository.CreateRecipeAsync(newRecipe);
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

    private async Task DeleteUploadFileAsync(string fileName)
    {
        await Task.Run(() =>
        {
            var filePath = Path.Combine(webEnv.GetUploadBasePath(), fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        });
    }
}