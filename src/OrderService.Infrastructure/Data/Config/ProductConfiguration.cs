using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.HasOne(p => p.productCategory).WithMany().IsRequired();

    builder.Property(p => p.productName).HasMaxLength(200).IsRequired();
    builder.Property(p => p.productImageUrl).IsRequired();
    builder.Property(p => p.productDescription).IsRequired();
    builder.Property(p => p.productPrice).IsRequired();
    builder.Property(p => p.productURL).IsRequired();
    builder.Property(p => p.productWeight).IsRequired();

    builder.Property(p => p.productSellerAddress).HasMaxLength(200).IsRequired();
    builder.Property(p => p.productSellerEmail).HasMaxLength(200).IsRequired();

    builder.Property(p => p.productWarrantable).IsRequired();
    builder.Property(p => p.productWarrantyDescription).IsRequired();
    builder.Property(p => p.productWarrantyDuration).IsRequired();

    builder.Property(p => p.productReturnable).IsRequired();
    builder.Property(p => p.productReturnDescription).IsRequired();
    builder.Property(p => p.productReturnDuration).IsRequired();
  }
}
