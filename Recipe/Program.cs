using Recipe.Features.Authentication;
using Recipe.Features.Onboarding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterAuthenticationFeature(builder.Configuration); // Registering the Authentication Feature
builder.Services.RegisterOnboardingFeature(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // added for the sake of the Authentication Feature
app.UseAuthorization();

app.MapControllers();

app.Run();