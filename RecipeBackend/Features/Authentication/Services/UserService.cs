using AutoMapper;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Authentication.Repositories;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Features.Authentication.Services;

public class UserService(
    UserRepository repository,
    IMapper mapper,
    IWebHostEnvironment webEnv,
    IHttpContextAccessor httpContextAccessor
) : ServiceBase("profiles", webEnv)
{
    public async Task<User> CreateUserAsync(UserCreateDto payload)
    {
        var user = mapper.Map<User>(payload);

        return await repository.CreateUser(user);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await repository.GetUserByIdAsync(id);
        DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
        return user;
    }

    public async Task<User?> GetUserByLoginAsync(string value)
    {
        var user = await repository.GetUserByLoginAsync(value);
        return user;
    }

    public async Task<User> UpdateUserAsync(int id, UserUpdateDto payload)
    {
        var user = await repository.GetUserByIdAsync(id);
        DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
        mapper.Map(payload, user);
        await repository.UpdateUserAsync();
        return user;
    }

    public async Task<User> UploadProfilePhotoAsync(int id, IFormFile profilePhoto)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext, $"Error accessing the HttpContext inside the {nameof(UserService)}");
        var user = await repository.GetUserByIdAsync(id);
        DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist.");
        var baseUrl = httpContextAccessor.HttpContext.GetUploadsBaseUrl();
        if (user.ProfilePhoto != null)
            DeleteUploadsFile(user.ProfilePhoto);
        user.ProfilePhoto = await SaveUploadsFileAsync(profilePhoto);
        await repository.UpdateUserAsync();
        user.ProfilePhoto = baseUrl + '/' + user.ProfilePhoto;
        return user;
    }
}