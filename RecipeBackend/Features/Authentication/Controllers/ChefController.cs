using Microsoft.AspNetCore.Mvc;

namespace RecipeBackend.Features.Authentication.Controllers;

[ApiController, Route("api/v1/chefs")]
public class ChefController : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> ListChefs()
    {
        return StatusCode(200);
    }
}