using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.Property(o => o.status)
      .HasConversion(
      s => s.Value,
      s => OrderStatus.FromValue(s)
      );

    builder.HasOne(o => o.user).WithMany(u => u.orders).HasForeignKey(o => o.userId);

    builder.Property(o => o.localShippingStatus)
        .HasConversion(
        s => s.Value,
        s => OrderLocalShippingStatus.FromValue(s)
      )
        .IsRequired()
        .HasDefaultValue(OrderLocalShippingStatus.notInQueue);

    builder.Property(o => o.price).IsRequired();
    builder.Property(o => o.customerDescription).HasMaxLength(300).IsRequired();
    builder.Property(o => o.shippingEstimatedDays).IsRequired();
    builder.Property(o => o.contactPhonenumber).IsRequired();
    builder.Property(o => o.deliveryAddress).IsRequired();
    builder.Property(o => o.orderDate).IsRequired();
    builder.Property(o => o.orderDescription).HasMaxLength(300).IsRequired();

  }
}
