﻿using System.ComponentModel.DataAnnotations;

namespace Recipe.Features.Authentication.DTOs;

public class LoginDto
{
    [MaxLength(64)]
    public required string Username { get; set; }

    [MaxLength(64)]
    public required string Password { get; set; }
}

public class UserCreateDto
{
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}