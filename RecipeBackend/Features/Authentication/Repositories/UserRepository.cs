﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task UpdateUserAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<bool> CheckUserExistsById(int id)
    {
        var exists = await context.Users.AnyAsync(u => u.Id == id);
        return exists;
    }

    public async Task<User?> GetUserByLoginAsync(string value)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == value || u.Username == value || u.PhoneNumber == value);
        return user;
    }

    public async Task<bool> CheckUserExistsByLoginAsync(string value)
    {
        var exists = await context.Users.AnyAsync(
            u => u.Email == value || u.PhoneNumber == value || u.Username == value
        );
        return exists;
    }
}