using System.Globalization;
using AutoMapper;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Authentication.Repositories;

namespace RecipeBackend.Features.Authentication.Services;

public class UserService(UserRepository repository, IMapper mapper)
{
    public async Task<User> CreateUserAsync(UserCreateDto payload)
    {
        payload.BirthDate = DateOnly.ParseExact(
            payload.BirthDate.ToString()!, "yyyy-MM-dd", CultureInfo.InvariantCulture
        );
        var user = mapper.Map<User>(payload);
        
        return await repository.CreateUser(user);
    }

    public async Task<User?> GetUserByLoginAsync(string value)
    {
        var user = await repository.GetUserByLoginAsync(value);
        return user;
    }
}