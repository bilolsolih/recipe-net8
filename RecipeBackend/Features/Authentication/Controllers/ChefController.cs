using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Filters;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Features.Authentication.Controllers;

[ApiController, Route("api/v1/top-chefs")]
public class ChefController(RecipeDbContext context, IMapper mapper) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<List<ChefListDto>>> ListChefs([FromQuery] ChefFilters filters)
    {
        
        var chefs = await context.Users.ProjectTo<ChefListDto>(mapper.ConfigurationProvider).ToListAsync();
        var baseUrl = HttpContext.GetUploadsBaseUrl();
        chefs.ForEach(chef =>
            chef.ProfilePhoto = chef.ProfilePhoto != null ? $"{baseUrl}/{chef.ProfilePhoto}" : string.Empty);
        return Ok(chefs);
    }
}