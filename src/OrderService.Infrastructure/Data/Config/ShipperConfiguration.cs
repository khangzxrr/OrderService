using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ShipperAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
{
  public void Configure(EntityTypeBuilder<Shipper> builder)
  {
    builder.HasOne(s => s.user).WithOne(u => u.shipper)
      .HasForeignKey<Shipper>(s => s.userId).OnDelete(DeleteBehavior.Restrict);

    builder.Property(s => s.shippingStatus)
      .HasConversion(
          s => s.Value,
          s => ShippingStatus.FromValue(s)
       ).IsRequired();
  }
}
