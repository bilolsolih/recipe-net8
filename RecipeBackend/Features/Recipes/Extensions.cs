using RecipeBackend.Features.Recipes.Repositories;
using RecipeBackend.Features.Recipes.Services;

namespace RecipeBackend.Features.Recipes;

public static class Extensions
{
    public static void RegisterRecipesFeature(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<CategoryRepository>();
        services.AddScoped<CategoryService>();

        services.AddScoped<RecipeService>();
        services.AddScoped<RecipeRepository>();
    }

    public static string GetUploadsBaseUrl(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return request.Scheme + "://" + request.Host + "/uploads";
    }

    public static string GetUploadBasePath(this IWebHostEnvironment env)
    {
        return Path.Combine(env.ContentRootPath, "uploads");
    }
}