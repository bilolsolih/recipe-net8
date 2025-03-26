using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Filters;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Repositories;

public class UserRepository(RecipeDbContext context, IMapper mapper)
{
  public async Task<User> AddAsync(User user)
  {
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return user;
  }

  public async Task<IEnumerable<User>> GetAllFollowersByIdAsync(int userId)
  {
    return await context.Users.Where(u => u.Id == userId).SelectMany(u => u.Followers).ToListAsync();
  }


  public async Task<IEnumerable<User>> GetAllFollowingsByIdAsync(int userId)
  {
    return await context.Users.Where(u => u.Id == userId).SelectMany(u => u.Followings).ToListAsync();
  }

  public async Task<User?> GetByIdAsync(int id)
  {
    return await context.Users.FindAsync(id);
  }

  public async Task UpdateAsync(User user)
  {
    user.Updated = DateTime.UtcNow;
    context.Users.Update(user);
    await context.SaveChangesAsync();
  }

  public async Task<bool> AddFollowerAsync(User user, User follower)
  {
    var newUserToUser = new UserToUser { UserId = user.Id, FollowerId = follower.Id, User = user, Follower = follower };
    context.UsersToUsers.Add(newUserToUser);
    await context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> RemoveFollowerAsync(User user, User follower)
  {
    var userToUser = await context.UsersToUsers.Where(u => u.UserId == user.Id && u.FollowerId == follower.Id).SingleAsync();
    context.UsersToUsers.Remove(userToUser);
    await context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsByIdAsync(int id)
  {
    var exists = await context.Users.AnyAsync(u => u.Id == id);
    return exists;
  }

  public async Task<User?> GetUserByLoginAsync(string value)
  {
    var user = await context.Users.SingleOrDefaultAsync(u => u.Email == value || u.Username == value || u.PhoneNumber == value);
    return user;
  }

  public async Task<bool> IsFollowingByIdAsync(int userId, int followerId)
  {
    return await context.Users.Where(u => u.Id == userId).SelectMany(u => u.Followers).AnyAsync(f => f.Id == followerId);
  }

  public async Task<bool> CheckUserExistsByLoginAsync(string value)
  {
    var exists = await context.Users.AnyAsync(
      u => u.Email == value || u.PhoneNumber == value || u.Username == value
    );
    return exists;
  }

  public async Task<List<TopChefSmall>> GetTopChefsAsync(UserFilters? filters)
  {
    var topChefs = context.Users.AsQueryable();
    if (filters != null)
    {
      if (filters is { Page: not null, Limit: not null }) topChefs = topChefs.Skip((int)((filters.Page - 1) * filters.Limit));
      if (filters.Limit != null) topChefs = topChefs.Take((int)filters.Limit);
    }

    var results = await topChefs.ProjectTo<TopChefSmall>(mapper.ConfigurationProvider).ToListAsync();

    return results;
  }
}