using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class InstructionConfigurations : IEntityTypeConfiguration<Instruction>
{
    public void Configure(EntityTypeBuilder<Instruction> builder)
    {
        builder.ToTable("Instruction");
        builder.HasKey(i => i.Id);
        builder.HasOne(i => i.Recipe).WithMany(r => r.Instructions);
        
        builder.Property(i => i.Text).IsRequired().HasMaxLength(256);
        
        builder.Property(i => i.Created)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAdd();

        builder.Property(i => i.Updated)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAddOrUpdate();
    }
    
}