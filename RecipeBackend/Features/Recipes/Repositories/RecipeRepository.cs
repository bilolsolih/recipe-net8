using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Repositories;

public class RecipeRepository(RecipeDbContext context, IMapper mapper)
{
    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();
        return recipe;
    }

    public async Task<List<RecipeListDto>> ListRecipesAsync(RecipeFilters? filters = null)
    {
        var filteredRecipes = context.Recipes.AsQueryable();
        if (filters?.Category != null)
        {
            filteredRecipes = filteredRecipes.Where(r => r.CategoryId == filters.Category);
        }

        var recipes = await filteredRecipes
            .ProjectTo<RecipeListDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        
        return recipes;
    }

    public async Task<RecipeDetailDto?> GetRecipeAsync(int id)
    {
        var recipe = await context.Recipes
            .Where(r => r.Id == id)
            .ProjectTo<RecipeDetailDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return recipe;
    }

    public async Task UpdateRecipeAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Recipe?> GetRecipeForUpdateAsync(int id)
    {
        var recipe = await context.Recipes.FindAsync(id);
        return recipe;
    }

    public async Task<bool> CheckRecipeExistsAsync(int id)
    {
        var exists = await context.Recipes.AnyAsync(r => r.Id == id);
        return exists;
    }
}