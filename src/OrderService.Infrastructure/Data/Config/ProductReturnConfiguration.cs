using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductReturnConfiguration : IEntityTypeConfiguration<ProductReturn>
{
  public void Configure(EntityTypeBuilder<ProductReturn> builder)
  {
    builder.Property(p => p.shippingStatus)
      .HasConversion(
        p => p.Value,
        p => ShippingStatus.FromValue(p)
      ).IsRequired();
  }
}
