using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core.Exceptions;
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
        var user = await service.GetUserByLoginAsync(payload.Login);
        DoesNotExistException.ThrowIfNull(user, $"User with username: {payload.Login} does not exist.");

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
        var user = await service.CreateUserAsync(payload);
        return Ok(user);
    }
}