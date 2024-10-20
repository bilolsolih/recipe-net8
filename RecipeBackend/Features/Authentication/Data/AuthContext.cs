using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Authentication.Configurations;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Data;

public class AuthContext(DbContextOptions<AuthContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfigurations());
    }
}