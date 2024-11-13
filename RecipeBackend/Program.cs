using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using RecipeBackend;
using RecipeBackend.Core;
using RecipeBackend.Features.Authentication;
using RecipeBackend.Features.Onboarding;
using RecipeBackend.Features.Recipes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add<CoreExceptionsFilter>());
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Recipe API", Version = "v1" });

    // Define JWT security scheme
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    // Add JWT requirement to all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor(); // Now we can access HttpContext outside of Controllers as well

builder.Services.RegisterAuthenticationFeature(builder.Configuration); // Registering the Authentication Feature
builder.Services.RegisterOnboardingFeature(builder.Configuration);
builder.Services.RegisterRecipesFeature(builder.Configuration);

builder.Services.AddNpgsql<RecipeDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

// these lines are needed to specify default path for static uploads
var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    RequestPath = "/uploads"
});

app.UseHttpsRedirection();

app.UseAuthentication(); // added for the sake of the Authentication Feature
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();