using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Customization.DTOs;
using RecipeBackend.Features.Customization.Models;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Features.Customization.Controllers;

[ApiController, Route("api/v1/cuisines")]
public class CuisineController(RecipeDbContext context, IMapper mapper, IWebHostEnvironment webEnv)
    : ControllerBase
{
    private string UploadsBaseAbsolutePath { get; } = webEnv.GetUploadBasePath();
    private string FolderName => "cuisines";

    [HttpPost("create")]
    public async Task<IActionResult> CreateCuisine(CuisineCreateDto payload)
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
        var newCuisine = new Cuisine
        {
            Title = payload.Title,
            Image = FolderName + '/' + payload.Image.FileName,
        };

        context.Cuisines.Add(newCuisine);
        await context.SaveChangesAsync();
        return StatusCode(201, newCuisine);
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListCuisines()
    {
        var cuisines = await context.Cuisines
            .ProjectTo<CuisineListDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        foreach (var cuisine in cuisines)
        {
            cuisine.Image = HttpContext.GetUploadsBaseUrl() + '/' + cuisine.Image;
        }

        return StatusCode(200, cuisines);
    }
}