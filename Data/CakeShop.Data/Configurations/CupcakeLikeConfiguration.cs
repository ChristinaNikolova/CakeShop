namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CupcakeLikeConfiguration : IEntityTypeConfiguration<CupcakeLike>
    {
        public void Configure(EntityTypeBuilder<CupcakeLike> cupcakeLike)
        {
            cupcakeLike
                .HasKey(cl => new { cl.CupcakeId, cl.ClientId });
        }
    }
}
