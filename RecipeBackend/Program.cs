using Microsoft.Extensions.FileProviders;
using RecipeBackend.Core;
using RecipeBackend.Features.Authentication;
using RecipeBackend.Features.Onboarding;
using RecipeBackend.Features.Recipes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); //options => options.Filters.Add<CoreExceptionsFilter>()
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor(); // Now we can access HttpContext outside of Controllers as well

builder.Services.RegisterAuthenticationFeature(builder.Configuration); // Registering the Authentication Feature
builder.Services.RegisterOnboardingFeature(builder.Configuration);
builder.Services.RegisterRecipesFeature(builder.Configuration);

builder.Services.AddNpgsql<RecipeDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

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

app.MapControllers();

app.Run();