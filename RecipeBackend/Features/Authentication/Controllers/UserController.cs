using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Authentication.Services;

namespace RecipeBackend.Features.Authentication.Controllers;

[ApiController, Route("api/v1/auth")]
public class UserController(UserService service, TokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto payload)
    {
        var user = await service.GetUserByEmail(payload.Login);
        if (user == null)
        {
            return NotFound(new { message = "User with this username doesn't exist." });
        }


        if (payload.Password == user.Password)
        {
            var token = await tokenService.GenerateTokenAsync(user.Email, user.Id);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserCreateDto payload)
    {
        var user = await service.CreateUser(payload);
        return Ok(user);
    }
}