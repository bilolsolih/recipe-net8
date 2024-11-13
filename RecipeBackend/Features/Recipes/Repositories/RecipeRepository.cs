using RecipeBackend;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Repositories;

public class RecipeRepository(RecipeDbContext context)
{
    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();
        return recipe;
    }
}