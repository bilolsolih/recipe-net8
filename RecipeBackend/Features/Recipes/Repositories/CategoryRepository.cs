using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Repositories;

public class CategoryRepository(RecipeDbContext context, IMapper mapper)
{
    public async Task<Category> CreateCategoryAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        return category;
    }

    public async Task<IList<CategoryListDto>> ListCategoriesAsync(CategoryFilters? filters = null)
    {
        var filteredCategories = context.Categories.AsQueryable();

        if (filters?.main != null)
        {
            filteredCategories = filteredCategories.Where(c => c.Main == filters.main);
        }

        var categories = await filteredCategories
            .ProjectTo<CategoryListDto>(mapper.ConfigurationProvider)
            .OrderBy(c => c.Title)
            .ToListAsync();

        return categories;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> CheckCategoryExistsAsync(string? title = null, int? id = null)
    {
        if (string.IsNullOrEmpty(title) && id == null)
        {
            throw new ArgumentException("'title' and/or 'id' must be passed to CheckCategoryExistsAsync method.");
        }

        var categories = context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(title))
        {
            categories = categories.Where(c => c.Title == title);
        }

        if (id.HasValue)
        {
            categories = categories.Where(c => c.Id == id);
        }

        return await categories.AnyAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task MakeAllCategoriesNonMain()
    {
        await context.Database.ExecuteSqlRawAsync("""UPDATE "Categories" SET "Main" = false WHERE "Main" = true""");
    }
}