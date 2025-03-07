using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Core.Filters;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Repositories;

public class RecipeRepository(RecipeDbContext context, IMapper mapper)
{
    public async Task<RecipeListDto?> GetTrendingRecipeAsync()
    {
        var recipe = await context.Recipes
            .OrderBy(r => r.Created)
            .Where(r => r.IsTrending).ProjectTo<RecipeListDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? await context.Recipes
            .OrderBy(r => r.Title)
            .ProjectTo<RecipeListDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return recipe;
    }

    public async Task<List<RecipeListDto>> ListTrendingRecipesAsync(PaginationFilters? filters)
    {
        var initialQuery = context.Recipes
            .OrderBy(r => r.Created)
            .Where(r => r.IsTrending)
            .AsQueryable();
        if (filters is { Page: not null, Limit: not null })
        {
            initialQuery = initialQuery.Skip((int)((filters.Page - 1) * filters.Limit));
        }

        if (filters is { Limit: not null })
        {
            initialQuery = initialQuery.Take((int)filters.Limit);
        }

        var result = await initialQuery.ProjectTo<RecipeListDto>(mapper.ConfigurationProvider).ToListAsync();

        return result;
    }

    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();
        return recipe;
    }

    public async Task<List<RecipeListDto>> ListRecipesAsync(RecipeFilters? filters)
    {
        var filteredRecipes = context.Recipes.AsQueryable();


        if (filters != null)
        {
            if (filters.Order != null)
            {
                filteredRecipes = filters.Order.ToLower() switch
                {
                    "title" => (bool)filters.Descending!
                        ? filteredRecipes.OrderByDescending(r => r.Title)
                        : filteredRecipes.OrderBy(r => r.Title),
                    "date" => (bool)filters.Descending!
                        ? filteredRecipes.OrderByDescending(r => r.Created)
                        : filteredRecipes.OrderBy(r => r.Created),
                    // _ => filteredRecipes.OrderBy(r => r.Title)
                };
            }

            if (filters.UserId != null) filteredRecipes = filteredRecipes.Where(r => r.UserId == filters.UserId);
            if (filters.Category != null)
                filteredRecipes = filteredRecipes.Where(r => r.CategoryId == filters.Category);
            if (filters.IsTrending != null)
                filteredRecipes = filteredRecipes.Where(r => r.IsTrending == filters.IsTrending);
            if (filters is { Page: not null, Limit: not null })
                filteredRecipes = filteredRecipes.Skip((int)((filters.Page - 1) * filters.Limit));
            if (filters.Limit != null) filteredRecipes = filteredRecipes.Take((int)filters.Limit);
        }


        var recipes = await filteredRecipes
            .ProjectTo<RecipeListDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return recipes;
    }

    public async Task<List<RecipeListCommunityDto>> ListCommunityRecipesAsync(RecipeFilters filters)
    {
        var filteredRecipes = context.Recipes.AsQueryable();

        if (filters.Order != null)
        {
            filteredRecipes = filters.Order.ToLower() switch
            {
                "title" => (bool)filters.Descending!
                    ? filteredRecipes.OrderByDescending(r => r.Title)
                    : filteredRecipes.OrderBy(r => r.Title),
                "date" => (bool)filters.Descending!
                    ? filteredRecipes.OrderByDescending(r => r.Created)
                    : filteredRecipes.OrderBy(r => r.Created),
                "rating" => (bool)filters.Descending!
                    ? filteredRecipes.OrderByDescending(r => r.Rating)
                    : filteredRecipes.OrderBy(r => r.Rating),
                _ => filteredRecipes.OrderBy(r => r.Title)
            };
        }

        if (filters.IsTrending != null)
            filteredRecipes = filteredRecipes.Where(r => r.IsTrending == filters.IsTrending);
        if (filters is { Page: not null, Limit: not null })
            filteredRecipes = filteredRecipes.Skip((int)((filters.Page - 1) * filters.Limit));
        if (filters.Limit != null) filteredRecipes = filteredRecipes.Take((int)filters.Limit);


        var recipes = await filteredRecipes
            .ProjectTo<RecipeListCommunityDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return recipes;
    }

    public async Task<List<RecipeListDto>> ListMyRecipesAsync(int userId, PaginationFilters? filters)
    {
        var myRecipes = context.Recipes.Where(r => r.UserId == userId).AsQueryable();
        if (filters != null)
        {
            if (filters is { Page: not null, Limit: not null })
            {
                myRecipes = myRecipes.Skip((int)((filters.Page - 1) * filters.Limit));
            }

            if (filters.Limit != null)
            {
                myRecipes = myRecipes.Take((int)filters.Limit);
            }
        }

        var result = await myRecipes.ProjectTo<RecipeListDto>(mapper.ConfigurationProvider).ToListAsync();
        return result;
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

    public async Task<int> GetRecipeCountAsync()
    {
        var count = await context.Recipes.CountAsync();
        return count;
    }

    public async Task<bool> CheckRecipeExistsAsync(int id)
    {
        var exists = await context.Recipes.AnyAsync(r => r.Id == id);
        return exists;
    }
}