using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderShippingAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class OrderShippingConfiguration : IEntityTypeConfiguration<OrderShipping>
{
  public void Configure(EntityTypeBuilder<OrderShipping> builder)
  {
    builder.Property(o => o.orderShippingStatus)
      .HasConversion(
      s => s.Value,
      s => OrderShippingStatus.FromValue(s)
      )
      .IsRequired();
  }
}
