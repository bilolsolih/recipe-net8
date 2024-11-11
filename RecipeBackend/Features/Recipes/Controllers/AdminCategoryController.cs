using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/admin/categories")]
[TypeFilter(typeof(CoreExceptionsFilter))]
public class AdminCategoryController(CategoryService service) : ControllerBase
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

    [HttpPatch("update/{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromForm] CategoryUpdateDto payload)
    {
        var updatedCategory = await service.UpdateCategoryAsync(id, payload);
        return StatusCode(200, updatedCategory);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await service.DeleteCategoryAsync(id);
        return StatusCode(204);
    }
}