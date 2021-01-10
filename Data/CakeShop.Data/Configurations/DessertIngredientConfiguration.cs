namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DessertIngredientConfiguration : IEntityTypeConfiguration<DessertIngredient>
    {
        public void Configure(EntityTypeBuilder<DessertIngredient> dessertIngredient)
        {
            dessertIngredient
                .HasKey(di => new { di.DessertId, di.IngredientId });
        }
    }
}
