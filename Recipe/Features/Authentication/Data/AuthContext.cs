using Microsoft.EntityFrameworkCore;
using Recipe.Features.Authentication.Configurations;
using Recipe.Features.Authentication.Models;

namespace Recipe.Features.Authentication.Data;

public class AuthContext(DbContextOptions<AuthContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfigurations());
    }
}