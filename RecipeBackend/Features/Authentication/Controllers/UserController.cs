﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Filters;
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
    if (user == null)
    {
      return Unauthorized();
    }

    if (payload.Password == user.Password)
    {
      var token = await tokenService.GenerateTokenAsync(user.Email, user.Id);
      return Ok(new { AccessToken = token });
    }

    return Unauthorized();
  }

  [HttpGet("me"), Authorize]
  public async Task<ActionResult<UserDetailDto>> GetMyDetails()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await service.GetUserByIdAsync(userId);
    return Ok(user);
  }

  [HttpPost("register")]
  public async Task<ActionResult<Dictionary<string, string>>> Register(UserCreateDto payload)
  {
    var user = await service.CreateUserAsync(payload);
    var token = await tokenService.GenerateTokenAsync(user.Email, user.Id);
    return StatusCode(201, new { AccessToken = token });
  }

  [HttpGet("details/{id:int}")]
  public async Task<IActionResult> GetUser(int id)
  {
    var user = await service.GetUserByIdAsync(id);
    return Ok(user);
  }

  [HttpPatch("update"), Authorize]
  public async Task<ActionResult<User>> Update(int id, UserUpdateDto payload)
  {
    var user = await service.UpdateUserAsync(id, payload);
    return Ok(user);
  }

  [HttpPatch("upload"), Authorize]
  public async Task<ActionResult<User>> UploadProfilePhoto(IFormFile profilePhoto)
  {
    var user = await service.UploadProfilePhotoAsync(int.Parse(User.FindFirstValue("userid")!), profilePhoto);
    return Ok(user);
  }

  [HttpGet("top-chefs")]
  public async Task<IActionResult> GetTopChefs([FromQuery] UserFilters? filters)
  {
    var topChefs = await service.GetTopChefsAsync(filters);
    return Ok(topChefs);
  }

  [HttpPost("follow/{id:int}"), Authorize]
  public async Task<ActionResult> FollowUser(int id)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    if (await service.StartFollowingAsync(id, userId))
    {
      return Ok();
    }

    return Ok("User already followed.");
  }

  [HttpPost("unfollow/{id:int}"), Authorize]
  public async Task<ActionResult> UnfollowUser(int id)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var result = await service.RemoveFollowerAsync(id, userId);
    return Ok(result ? "Unfollowed." : "Does not exist.");
  }

  [HttpPost("remove-follower/{id:int}")]
  public async Task<ActionResult> RemoveFollower(int id)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var result = await service.RemoveFollowerAsync(userId, id);
    return Ok(result ? "Removed." : "Does not exist.");
  }

  [HttpGet("followers/{id:int}")]
  public async Task<ActionResult<IEnumerable<UserListDto>>> GetAllFollowers(int id)
  {
    var allFollowers = await service.GetAllFollowersById(id);
    return Ok(allFollowers);
  }
  
  [HttpGet("followings/{id:int}")]
  public async Task<ActionResult<IEnumerable<UserListDto>>> GetAllFollowings(int id)
  {
    var allFollowings = await service.GetAllFollowingsById(id);
    return Ok(allFollowings);
  }
}