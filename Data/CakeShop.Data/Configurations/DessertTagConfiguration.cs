namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DessertTagConfiguration : IEntityTypeConfiguration<DessertTag>
    {
        public void Configure(EntityTypeBuilder<DessertTag> dessertTag)
        {
            dessertTag
                .HasKey(dt => new { dt.DessertId, dt.TagId });
        }
    }
}
