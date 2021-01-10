namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CupcakeIngredientConfiguration : IEntityTypeConfiguration<CupcakeIngredient>
    {
        public void Configure(EntityTypeBuilder<CupcakeIngredient> cupcakeIngredient)
        {
            cupcakeIngredient
                .HasKey(ci => new { ci.CupcakeId, ci.IngredientId });
        }
    }
}
