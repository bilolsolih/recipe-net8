using AutoMapper;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Filters;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Authentication.Repositories;

namespace RecipeBackend.Features.Authentication.Services;

public class UserService(
    UserRepository repository,
    IMapper mapper,
    IWebHostEnvironment webEnv,
    IHttpContextAccessor httpContextAccessor
) : ServiceBase("profiles", webEnv, httpContextAccessor)
{
    public async Task<User> CreateUserAsync(UserCreateDto payload)
    {
        var user = mapper.Map<User>(payload);

        return await repository.CreateUser(user);
    }

    public async Task<UserDetailDto> GetUserByIdAsync(int id)
    {
        var user = await repository.GetUserByIdAsync(id);
        DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
        if (user.ProfilePhoto != null)
        {
            user.ProfilePhoto = $"{BaseUrl}/{user.ProfilePhoto}";
        }

        return mapper.Map<UserDetailDto>(user);
    }

    public async Task<User?> GetUserByLoginAsync(string value)
    {
        var user = await repository.GetUserByLoginAsync(value);
        return user;
    }

    public async Task<UserDetailDto> UpdateUserAsync(int id, UserUpdateDto payload)
    {
        var user = await repository.GetUserByIdAsync(id);
        DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
        mapper.Map(payload, user);
        await repository.UpdateUserAsync();

        return mapper.Map<UserDetailDto>(user);
    }

    public async Task<UserDetailDto> UploadProfilePhotoAsync(int id, IFormFile profilePhoto)
    {
        var user = await repository.GetUserByIdAsync(id);
        DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist.");
        if (user.ProfilePhoto != null)
            DeleteUploadsFile(user.ProfilePhoto);
        user.ProfilePhoto = await SaveUploadsFileAsync(profilePhoto);
        await repository.UpdateUserAsync();
        user.ProfilePhoto = $"{BaseUrl}/{user.ProfilePhoto}";
        return mapper.Map<UserDetailDto>(user);
    }

    public async Task<IEnumerable<TopChefSmall>> GetTopChefsAsync(UserFilters? filters)
    {
        var topChefs = await repository.GetTopChefsAsync(filters);
        topChefs.ForEach(topChef =>
        {
            if (topChef.Photo != null) topChef.Photo = $"{BaseUrl}/{topChef.Photo}";
        });

        return topChefs;
    }
}