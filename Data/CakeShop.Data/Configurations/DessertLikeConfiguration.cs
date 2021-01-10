namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DessertLikeConfiguration : IEntityTypeConfiguration<DessertLike>
    {
        public void Configure(EntityTypeBuilder<DessertLike> dessertLike)
        {
            dessertLike
                .HasKey(dl => new { dl.DessertId, dl.ClientId });
        }
    }
}
