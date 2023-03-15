using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductShipCostConfiguration : IEntityTypeConfiguration<ProductShipCost>
{
  public void Configure(EntityTypeBuilder<ProductShipCost> builder)
  {
    builder.Property(p =>p.shipCost).IsRequired();  
    builder.Property(p => p.costPerWeight).IsRequired();
  }
}
