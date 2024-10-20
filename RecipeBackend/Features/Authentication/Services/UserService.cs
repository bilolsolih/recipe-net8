using AutoMapper;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Authentication.Repositories;

namespace RecipeBackend.Features.Authentication.Services;

public class UserService(UserRepository repository, IMapper mapper)
{
    public async Task<User> CreateUser(UserCreateDto payload)
    {
        var user = mapper.Map<User>(payload);
        return await repository.CreateUser(user);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await repository.GetUserByEmail(email);
        return user;
    }
}