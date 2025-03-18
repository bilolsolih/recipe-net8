namespace RecipeBackend.Features.Authentication.DTOs;

public class ChefListDto
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePhoto { get; set; }
}