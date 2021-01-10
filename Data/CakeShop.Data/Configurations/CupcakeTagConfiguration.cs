namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CupcakeTagConfiguration : IEntityTypeConfiguration<CupcakeTag>
    {
        public void Configure(EntityTypeBuilder<CupcakeTag> cupcakeTag)
        {
            cupcakeTag
                .HasKey(ct => new { ct.CupcakeId, ct.TagId });
        }
    }
}
