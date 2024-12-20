﻿using System.ComponentModel.DataAnnotations;

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
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required object BirthDate { get; set; }
    public required string Password { get; set; }
}

public class UserCompleteDto
{
    public FormFile ProfilePhoto { get; set; }
    public string Gender { get; set; }
    public string Bio { get; set; }
}