using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core.Filters;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/recipes")]
public class MobileRecipeController(RecipeService service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateRecipe(RecipeCreateDto payload)
    {
        var userId = int.Parse(User.FindFirstValue("userid")!);
        var newRecipe = await service.CreateRecipeAsync(payload, userId);
        return StatusCode(201, newRecipe);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListRecipes([FromQuery] RecipeFilters? filters)
    {
        var recipes = await service.ListRecipesAsync(filters);
        return StatusCode(200, recipes);
    }

    [HttpGet("trending-recipe")]
    public async Task<IActionResult> GetTrendingRecipes()
    {
        var trendingRecipes = await service.GetTrendingRecipeAsync();
        return Ok(trendingRecipes);
    }

    [HttpGet("trending-recipes")]
    public async Task<IActionResult> ListTrendingRecipes([FromQuery]PaginationFilters? filters)
    {
        var trendingRecipes = await service.ListTrendingRecipesAsync(filters);
        return Ok(trendingRecipes);
    }

    [HttpGet("detail/{id:int}")]
    public async Task<IActionResult> GetRecipe(int id)
    {
        var recipe = await service.GetRecipeAsync(id);
        return StatusCode(200, recipe);
    }

    [HttpPatch("update/{id:int}")]
    public async Task<IActionResult> UpdateRecipe(int id, RecipeUpdateDto payload)
    {
        var updatedRecipe = await service.UpdateRecipeAsync(id, payload);
        return StatusCode(200, updatedRecipe);
    }

    [HttpGet("my-recipes")]
    public async Task<IActionResult> GetMyRecipes([FromQuery] PaginationFilters? filters)
    {
        var userId = int.Parse(User.FindFirstValue("userid")!);

        var myRecipes = await service.ListMyRecipesAsync(userId, filters);
        return Ok(myRecipes);
    }
}