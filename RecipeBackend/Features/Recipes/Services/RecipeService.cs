using AutoMapper;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Repositories;

namespace RecipeBackend.Features.Recipes.Services;

public class RecipeService(RecipeRepository repository, IMapper mapper, IWebHostEnvironment webEnv)
{
    public async Task<Recipe> CreateRecipeAsync(RecipeCreateDto payload, int userId)
    {
        var newRecipe = mapper.Map<Recipe>(payload);
        
        var videoRecipe = await HandleRecipeVideoAsync(payload.VideoRecipe);   
        newRecipe.VideoRecipe = videoRecipe;
        newRecipe.UserId = userId;
        
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
        UploadedFileInvalidException.ThrowIfNull(fileExtension, $"Uploaded file {file.FileName} does not have an extension.");
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