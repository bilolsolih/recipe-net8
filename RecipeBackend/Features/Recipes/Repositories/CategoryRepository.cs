using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes.Data;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Repositories;

public class CategoryRepository(RecipeContext context, IMapper mapper)
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

    public async Task<IList<CategoryListDto>> ListCategoriesAsync()
    {
        var categories = await context.Categories
                                      .ProjectTo<CategoryListDto>(mapper.ConfigurationProvider)
                                      .OrderBy(c => c.Title)
                                      .ToListAsync();
        return categories;
    }
}