using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/categories")]
public class CategoryController(CategoryService service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateCategoryAsync([FromForm] CategoryCreateDto payload)
    {
        var newCategory = await service.CreateCategoryAsync(payload);
        return StatusCode(201, newCategory);
    }

    [HttpGet("detail/{id:int}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await service.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return StatusCode(404, $"Category with id {id} not found.");
        }

        var baseUrl = HttpContext.Request;

        return StatusCode(200, category);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListCategories()
    {
        var categories = await service.ListCategoriesAsync();
        return StatusCode(200, categories);
    }
}