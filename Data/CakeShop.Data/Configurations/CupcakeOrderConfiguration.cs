namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CupcakeOrderConfiguration : IEntityTypeConfiguration<CupcakeOrder>
    {
        public void Configure(EntityTypeBuilder<CupcakeOrder> cupcakeOrder)
        {
            cupcakeOrder
                .HasKey(co => new { co.CupcakeId, co.OrderId });
        }
    }
}
