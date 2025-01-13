using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Features.Authentication.Controllers;

[ApiController, Route("api/v1/chefs")]
public class ChefController(RecipeDbContext context) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> ListChefs()
    {
        var chefs = await context.Users.ToListAsync();
        var baseUrl = HttpContext.GetUploadsBaseUrl();
        foreach (var chef in chefs)
        {
            chef.ProfilePhoto = baseUrl + '/' + chef.ProfilePhoto;
        }

        return StatusCode(200, chefs);
    }
}