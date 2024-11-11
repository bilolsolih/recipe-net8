using AutoMapper;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Repositories;

namespace RecipeBackend.Features.Recipes.Services;

public class CategoryService(CategoryRepository repo, IMapper mapper, IWebHostEnvironment webEnv, IHttpContextAccessor httpContext)
{
    public async Task<Category> CreateCategoryAsync(CategoryCreateDto payload)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        if (await repo.CheckCategoryExistsAsync(title: payload.Title))
        {
            throw new AlreadyExistsException($"{nameof(Category)} with unique column {nameof(payload.Title)}: {payload.Title} already exists.");
        }

        var fileName = await HandleCategoryPhotoAsync(payload.Photo);
        var newCategory = new Category
        {
            Title = payload.Title,
            Photo = fileName
        };
        return await repo.CreateCategoryAsync(newCategory);
    }

    public async Task<CategoryDetailDto?> GetCategoryByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        var category = await repo.GetCategoryByIdAsync(id);
        DoesNotExistException.ThrowIfNull(category, $"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");

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

    public async Task<Category> UpdateCategoryAsync(int id, CategoryUpdateDto payload)
    {
        if (!await repo.CheckCategoryExistsAsync(id: id))
        {
            throw new DoesNotExistException($"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");
        }

        if (payload.Title != null && await repo.CheckCategoryExistsAsync(title: payload.Title))
        {
            throw new AlreadyExistsException($"{nameof(Category)} with unique column {nameof(payload.Title)}: {payload.Title} already exists.");
        }

        var category = await repo.GetCategoryByIdAsync(id);

        if (payload.Title != null)
        {
            category!.Title = payload.Title;
        }

        if (payload.Photo != null)
        {
            await DeleteCategoryPhotoAsync(category!.Photo);
            var fileName = await HandleCategoryPhotoAsync(payload.Photo);
            category.Photo = fileName;
        }

        await repo.UpdateCategoryAsync();

        return category!;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await repo.GetCategoryByIdAsync(id);
        DoesNotExistException.ThrowIfNull(category, $"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");
        
        await DeleteCategoryPhotoAsync(category!.Photo);
        await repo.DeleteCategoryAsync(category);
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

    private async Task DeleteCategoryPhotoAsync(string fileName)
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