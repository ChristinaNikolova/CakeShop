namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RepiceIngredientConfiguration : IEntityTypeConfiguration<RepiceIngredient>
    {
        public void Configure(EntityTypeBuilder<RepiceIngredient> repiceIngredient)
        {
            repiceIngredient
                .HasKey(ri => new { ri.IngredientId, ri.RecipeId });
        }
    }
}
