
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductIssueAggregate;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class IssueStateTrackingConfiguration : IEntityTypeConfiguration<IssueStateTracking>
{
  public void Configure(EntityTypeBuilder<IssueStateTracking> builder)
  {
    builder.Property(p => p.productIssueStatus)
      .HasConversion(
        p => p.Value,
        p => ProductIssueStatus.FromValue(p)
      ).IsRequired();
  }
}
