
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.ProductIssueAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductIssueShippingConfiguration : IEntityTypeConfiguration<ProductIssueShipping>
{
  public void Configure(EntityTypeBuilder<ProductIssueShipping> builder)
  {
    builder.Property(o => o.shippingStatus)
      .HasConversion(
      s => s.Value,
      s => OrderShippingStatus.FromValue(s)
      )
      .IsRequired();

    builder.HasOne(o => o.shipper);
  }
}
