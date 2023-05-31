
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class IssuePaymentConfiguration : IEntityTypeConfiguration<IssuePayment>
{
  public void Configure(EntityTypeBuilder<IssuePayment> builder)
  {
    builder.Property(p => p.paymentStatus)
      .HasConversion(
        p => p.Value,
        p => IssuePaymentStatus.FromValue(p)
      ).IsRequired();
  }
}
