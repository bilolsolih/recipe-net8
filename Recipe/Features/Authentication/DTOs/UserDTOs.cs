using System.ComponentModel.DataAnnotations;

namespace Recipe.Features.Authentication.DTOs;

public class LoginDto
{
    [MaxLength(64)]
    public required string Login { get; set; }

    [MaxLength(64)]
    public required string Password { get; set; }
}

public class UserCreateDto
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required string Password { get; set; }
}