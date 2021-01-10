namespace CakeShop.Data.Configurations
{
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DessertOrderConfiguration : IEntityTypeConfiguration<DessertOrder>
    {
        public void Configure(EntityTypeBuilder<DessertOrder> dessertOrder)
        {
            dessertOrder
                .HasKey(dor => new { dor.DessertId, dor.OrderId });
        }
    }
}
