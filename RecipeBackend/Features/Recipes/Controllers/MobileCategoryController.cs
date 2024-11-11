﻿using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/categories")]
[TypeFilter(typeof(CoreExceptionsFilter))]
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
    public async Task<IActionResult> ListCategories()
    {
        var categories = await service.ListCategoriesAsync();
        return StatusCode(200, categories);
    }
}