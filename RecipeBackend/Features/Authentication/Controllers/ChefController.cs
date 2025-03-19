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
        var chefsQuery = context.Users.AsQueryable();
        if (filters is { Page: not null, Limit: not null })
        {
            chefsQuery = chefsQuery.Skip((int)(filters.Limit * (filters.Page - 1)));
        }

        if (filters is { Limit: not null })
        {
            chefsQuery = chefsQuery.Take((int)filters.Limit);
        }

        if (filters is { Order: not null })
        {
            chefsQuery = filters.Order switch
            {
                OrderBy.Date => filters.Descending
                    ? chefsQuery.OrderByDescending(chef => chef.Created)
                    : chefsQuery.OrderBy(chef => chef.Created),
                _ => chefsQuery
            };
        }

        var chefs = await chefsQuery.ProjectTo<ChefListDto>(mapper.ConfigurationProvider).ToListAsync();
        var baseUrl = HttpContext.GetUploadsBaseUrl();

        chefs.ForEach(chef =>
            chef.ProfilePhoto = chef.ProfilePhoto != null ? $"{baseUrl}/{chef.ProfilePhoto}" : string.Empty);

        return Ok(chefs);
    }
}