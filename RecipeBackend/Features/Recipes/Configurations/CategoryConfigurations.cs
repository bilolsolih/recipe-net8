﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(c => c.Title)
               .IsUnique();

        builder.Property(c => c.Title)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(c => c.Photo)
               .IsRequired();


    }
}