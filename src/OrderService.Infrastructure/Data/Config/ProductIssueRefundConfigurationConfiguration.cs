
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductIssueAggregate;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductIssueRefundConfigurationConfiguration : IEntityTypeConfiguration<ProductIssueRefundConfiguration>
{
  public void Configure(EntityTypeBuilder<ProductIssueRefundConfiguration> builder)
  {
    builder.Property(p => p.productIssueStatus)
      .HasConversion(
        p => p.Value,
        p => ProductIssueStatus.FromValue(p)
      ).IsRequired().HasDefaultValue(ProductIssueStatus.request);
  }
}
