namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RecipeLikeConfiguration : IEntityTypeConfiguration<RecipeLike>
    {
        public void Configure(EntityTypeBuilder<RecipeLike> recipeLike)
        {
            recipeLike
                .HasKey(rl => new { rl.RecipeId, rl.ClientId });
        }
    }
}
