﻿using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Recipes.Models;

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

public class Recipe
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public string? VideoRecipe { get; set; }
    public int TimeRequired { get; set; }
    public bool IsTrending { set; get; }
    public double Rating { get; set; }
    public required Difficulty Difficulty { get; set; }


    public User User { get; set; }
    public ICollection<User> LikedUsers { get; set; } = new List<User>();
    public Category Category { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
    public ICollection<Instruction>? Instructions { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}