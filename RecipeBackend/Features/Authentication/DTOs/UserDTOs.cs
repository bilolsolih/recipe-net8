using System.ComponentModel.DataAnnotations;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.DTOs;

public class LoginDto
{
    [MaxLength(64)]
    public required string Login { get; set; }

    [MaxLength(64)]
    public required string Password { get; set; }
}

public class UserCreateDto
{
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string BirthDate { get; set; }
    public required string Password { get; set; }
}

public class UserDetailDto
{
    public required int Id { get; set; }
    public string? ProfilePhoto { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Presentation { get; set; }
    public required int RecipesCount { get; set; }
    public required int FollowingCount { get; set; } = 0;
    public required int FollowerCount { get; set; } = 0;
}

public class UserUpdateDto
{
    public string? Gender { get; set; }
    public string? Bio { get; set; }

    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BirthDate { get; set; }
}

public class TopChefSmall
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public string? Photo { get; set; }
}