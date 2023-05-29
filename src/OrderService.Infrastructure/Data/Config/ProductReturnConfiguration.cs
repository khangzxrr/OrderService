using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductReturnConfiguration : IEntityTypeConfiguration<ProductReturn>
{
  public void Configure(EntityTypeBuilder<ProductReturn> builder)
  {
    builder.Property(p => p.status)
      .HasConversion(
        p => p.Value,
        p => ProductReturnStatus.FromValue(p)
      ).IsRequired();

  }
}
