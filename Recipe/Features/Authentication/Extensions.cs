using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Recipe.Features.Authentication.Data;
using Recipe.Features.Authentication.Repositories;
using Recipe.Features.Authentication.Services;

namespace Recipe.Features.Authentication;

public static class AuthenticationExtensions
{
    public static void RegisterAuthenticationFeature(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var jwtSettings = config.GetSection("JwtSettings");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddNpgsql<AuthContext>(config.GetConnectionString("DefaultConnection"));

        services.AddScoped<TokenService>();
        services.AddScoped<UserService>();
        services.AddScoped<UserRepository>();

        services.AddAutoMapper(typeof(Program));
    }
}