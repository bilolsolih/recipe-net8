namespace RecipeBackend.Features.Authentication.Models;

public class UserToUser
{
  public required int UserId { get; set; }
  public required int FollowerId { get; set; }

  public required User User { get; set; }
  public required User Follower { get; set; }
}