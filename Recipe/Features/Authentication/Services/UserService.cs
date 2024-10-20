using AutoMapper;
using Recipe.Features.Authentication.DTOs;
using Recipe.Features.Authentication.Models;
using Recipe.Features.Authentication.Repositories;

namespace Recipe.Features.Authentication.Services;

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