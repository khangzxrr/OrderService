using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class OrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
{
  public void Configure(EntityTypeBuilder<OrderPayment> builder)
  {
    builder.Property(o => o.paymentCost).IsRequired();
    builder.Property(o => o.paymentDate).IsRequired();
    builder.Property(o => o.paymentDescription).HasMaxLength(300).IsRequired();
    builder.Property(o => o.paymentStatus).HasConversion(
        s => s.Value,
        s => PaymentStatus.FromValue(s)
      );


  }
}
