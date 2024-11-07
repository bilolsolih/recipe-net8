using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Repositories;

namespace RecipeBackend.Features.Recipes.Services;

public class CategoryService(CategoryRepository repo, IMapper mapper, IWebHostEnvironment webEnv, IHttpContextAccessor httpContext)
{
    public async Task<Category> CreateCategoryAsync(CategoryCreateDto payload)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        var fileName = await HandleCategoryPhotoAsync(payload.Photo);
        var newCategory = new Category
        {
            Title = payload.Title,
            Photo = fileName
        };

        try
        {
            return await repo.CreateCategoryAsync(newCategory);
        }
        catch (DbUpdateException)
        {
            var filePath = Path.Combine(webEnv.GetUploadBasePath(), fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            throw;
        }
    }

    public async Task<CategoryDetailDto?> GetCategoryByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        var category = await repo.GetCategoryByIdAsync(id);
        var categoryDetailDto = mapper.Map<CategoryDetailDto>(category);
        categoryDetailDto.Photo = httpContext.HttpContext.GetUploadsBaseUrl() + '/' + categoryDetailDto.Photo;
        return categoryDetailDto;
    }

    public async Task<ICollection<CategoryListDto>> ListCategoriesAsync()
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        var categories = await repo.ListCategoriesAsync();

        foreach (var category in categories)
        {
            category.Photo = httpContext.HttpContext.GetUploadsBaseUrl() + '/' + category.Photo;
        }

        return categories;
    }

    private async Task<string> HandleCategoryPhotoAsync(IFormFile photo)
    {
        var rootPath = webEnv.ContentRootPath;
        var lastPeriodIndex = photo.FileName.LastIndexOf('.');
        var fileName = lastPeriodIndex == -1 ? photo.FileName : photo.FileName[..lastPeriodIndex] + GenerateShortGuid();
        fileName += GetFileExtension(photo);

        var filePath = Path.Combine(rootPath, "uploads", "categories", fileName);

        if (!Directory.Exists(Path.Combine(rootPath, "uploads")))
        {
            Directory.CreateDirectory(Path.Combine(rootPath, "uploads"));
        }

        if (!Directory.Exists(Path.Combine(rootPath, "uploads", "categories")))
        {
            Directory.CreateDirectory(Path.Combine(rootPath, "uploads", "categories"));
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await photo.CopyToAsync(fileStream);

        var relativeFilePath = "categories" + '/' + fileName;

        return relativeFilePath;
    }

    private string GetFileExtension(IFormFile photo)
    {
        var fileExtension = Path.GetExtension(photo.FileName);
        return string.IsNullOrEmpty(fileExtension) ? ".png" : fileExtension;
    }

    private string GenerateShortGuid(int length = 8)
    {
        var guid = Guid.NewGuid().ToString("N");
        return guid[..length];
    }
}