using AutoMapper;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Filters;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Authentication.Repositories;

namespace RecipeBackend.Features.Authentication.Services;

public class UserService(
  UserRepository userRepo,
  IMapper mapper,
  IWebHostEnvironment webEnv,
  IHttpContextAccessor httpContextAccessor
) : ServiceBase("profiles", webEnv, httpContextAccessor)
{
  public async Task<User> CreateUserAsync(UserCreateDto payload)
  {
    var user = mapper.Map<User>(payload);

    return await userRepo.AddAsync(user);
  }


  public async Task<UserDetailDto> GetUserByIdAsync(int id)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
    if (user.ProfilePhoto != null)
    {
      user.ProfilePhoto = $"{BaseUrl}/{user.ProfilePhoto}";
    }

    return mapper.Map<UserDetailDto>(user);
  }

  public async Task<User?> GetUserByLoginAsync(string value)
  {
    var user = await userRepo.GetUserByLoginAsync(value);
    return user;
  }

  public async Task<UserDetailDto> UpdateUserAsync(int id, UserUpdateDto payload)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
    mapper.Map(payload, user);
    await userRepo.UpdateAsync(user);

    return mapper.Map<UserDetailDto>(user);
  }

  public async Task<UserDetailDto> UploadProfilePhotoAsync(int id, IFormFile profilePhoto)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist.");
    if (user.ProfilePhoto != null)
      DeleteUploadsFile(user.ProfilePhoto);
    user.ProfilePhoto = await SaveUploadsFileAsync(profilePhoto);
    await userRepo.UpdateAsync(user);
    user.ProfilePhoto = $"{BaseUrl}/{user.ProfilePhoto}";
    return mapper.Map<UserDetailDto>(user);
  }

  public async Task<IEnumerable<TopChefSmall>> GetTopChefsAsync(UserFilters? filters)
  {
    var topChefs = await userRepo.GetTopChefsAsync(filters);
    topChefs.ForEach(
      topChef =>
      {
        if (topChef.Photo != null) topChef.Photo = $"{BaseUrl}/{topChef.Photo}";
      }
    );

    return topChefs;
  }

  public async Task<IEnumerable<UserListDto>> GetAllFollowersById(int userId)
  {
    var allFollowers = await userRepo.GetAllFollowersByIdAsync(userId);
    var followers = mapper.Map<List<UserListDto>>(allFollowers);
    followers.ForEach(
      follower =>
      {
        if (follower.ProfilePhoto != null)
          follower.ProfilePhoto = $"{BaseUrl}/{follower.ProfilePhoto}";
      }
    );

    return followers;
  }

  public async Task<IEnumerable<UserListDto>> GetAllFollowingsById(int userId)
  {
    var allFollowings = await userRepo.GetAllFollowingsByIdAsync(userId);
    var followings = mapper.Map<List<UserListDto>>(allFollowings);
    followings.ForEach(
      following =>
      {
        if (following.ProfilePhoto != null)
          following.ProfilePhoto = $"{BaseUrl}/{following.ProfilePhoto}";
      }
    );

    return followings;
  }

  public async Task<bool> StartFollowingAsync(int userId, int followerId)
  {
    var user = await userRepo.GetByIdAsync(userId);
    DoesNotExistException.ThrowIfNull(user, nameof(User));

    var followerUser = await userRepo.GetByIdAsync(followerId);
    DoesNotExistException.ThrowIfNull(followerUser, nameof(User));

    var alreadyFollowing = await userRepo.IsFollowingByIdAsync(userId, followerId);

    if (alreadyFollowing)
      return false;

    return await userRepo.AddFollowerAsync(user, followerUser);
  }


  public async Task<bool> RemoveFollowerAsync(int userId, int followerId)
  {
    var user = await userRepo.GetByIdAsync(userId);
    DoesNotExistException.ThrowIfNull(user, nameof(User));

    var followerUser = await userRepo.GetByIdAsync(followerId);
    DoesNotExistException.ThrowIfNull(followerUser, nameof(User));

    var isFollowing = await userRepo.IsFollowingByIdAsync(userId, followerId);

    if (!isFollowing)
      return false;

    return await userRepo.RemoveFollowerAsync(user, followerUser);
  }
}