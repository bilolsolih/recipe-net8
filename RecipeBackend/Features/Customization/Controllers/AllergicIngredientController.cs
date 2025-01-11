using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Customization.DTOs;
using RecipeBackend.Features.Customization.Models;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Features.Customization.Controllers;

[ApiController, Route("api/v1/allergic")]
public class AllergicIngredientController(RecipeDbContext context, IMapper mapper, IWebHostEnvironment webEnv) : ControllerBase
{
    private string UploadsBaseAbsolutePath { get; } = webEnv.GetUploadBasePath();
    private string FolderName => "allergic";

    [HttpPost("create")]
    public async Task<IActionResult> CreateAllergicIngredient(AllergicIngredientCreateDto payload)
    {
        if (!Directory.Exists(UploadsBaseAbsolutePath))
        {
            Directory.CreateDirectory(UploadsBaseAbsolutePath);
        }

        if (!Directory.Exists(Path.Combine(UploadsBaseAbsolutePath, FolderName)))
        {
            Directory.CreateDirectory(Path.Combine(UploadsBaseAbsolutePath, FolderName));
        }

        await using var ingredientImage = new FileStream(Path.Combine(UploadsBaseAbsolutePath, FolderName, payload.Image.FileName), FileMode.Create);
        await payload.Image.CopyToAsync(ingredientImage);
        var newIngredient = new AllergicIngredient
        {
            Title = payload.Title,
            Image = FolderName + '/' + payload.Image.FileName
        };

        context.AllergicIngredients.Add(newIngredient);
        await context.SaveChangesAsync();
        return StatusCode(201, newIngredient);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListAllergicIngredients()
    {
        var ingredients = await context.AllergicIngredients
            .ProjectTo<AllergicIngredientListDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var ingredient in ingredients)
        {
            ingredient.Image = HttpContext.GetUploadsBaseUrl() + '/' + ingredient.Image;
        }

        return StatusCode(200, ingredients);
    }
}