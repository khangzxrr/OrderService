
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
internal class ReturnPaymentConfiguration : IEntityTypeConfiguration<ReturnPayment>
{
  public void Configure(EntityTypeBuilder<ReturnPayment> builder)
  {
    builder.Property(p => p.paymentStatus)
      .HasConversion(
        p => p.Value,
        p => ReturnPaymentStatus.FromValue(p)
      ).IsRequired();
  }
}
