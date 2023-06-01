using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductIssueConfiguration : IEntityTypeConfiguration<ProductIssue>
{
  public void Configure(EntityTypeBuilder<ProductIssue> builder)
  {
    builder.Property(p => p.status)
      .HasConversion(
        p => p.Value,
        p => ProductIssueStatus.FromValue(p)
      ).IsRequired().HasDefaultValue(ProductIssueStatus.request);

    builder.HasOne(pr => pr.product).WithMany();
    builder.HasOne(pr => pr.assignedEmployee).WithMany().OnDelete(DeleteBehavior.Restrict);

  }
}
