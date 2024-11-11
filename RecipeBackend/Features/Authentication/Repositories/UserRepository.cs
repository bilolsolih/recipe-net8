using Microsoft.EntityFrameworkCore;
using RecipeBackend.Core;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Repositories;

public class UserRepository(RecipeDbContext context)
{
    public async Task<User> CreateUser(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUser(int id)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);
        return user;
    }
}