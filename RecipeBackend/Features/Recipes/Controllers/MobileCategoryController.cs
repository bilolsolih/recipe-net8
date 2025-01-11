using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/categories")]
public class MobileCategoryController(CategoryService service) : ControllerBase
{
    [HttpGet("detail/{id:int}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await service.GetCategoryByIdAsync(id);
        DoesNotExistException.ThrowIfNull(category, $"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");

        return StatusCode(200, category);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListCategories([FromQuery] CategoryFilters filters)
    {
        // await Task.Delay(5000);
        var categories = await service.ListCategoriesAsync(filters);
        return StatusCode(200, categories);
    }
}