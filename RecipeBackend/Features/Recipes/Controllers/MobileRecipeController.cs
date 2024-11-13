using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/recipes"), Authorize]
public class MobileRecipeController(RecipeService service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateRecipe([FromForm] RecipeCreateDto payload)
    {
        var userId = int.Parse(User.FindFirstValue("userid")!);
        var newRecipe = await service.CreateRecipeAsync(payload, userId);
        return StatusCode(201, newRecipe);
    }
}