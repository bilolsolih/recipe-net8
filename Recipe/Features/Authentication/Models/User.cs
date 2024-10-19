namespace Recipe.Features.Authentication.Models;

public enum Gender
{
    Male,
    Female
}

public class User
{
    public int Id { get; set; }
    public string? ProfilePhoto { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string Password { get; set; }
}