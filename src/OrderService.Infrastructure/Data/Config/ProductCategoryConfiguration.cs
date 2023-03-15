using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
  public void Configure(EntityTypeBuilder<ProductCategory> builder)
  {
    builder.Property(p => p.productCategoryName).HasMaxLength(100).IsRequired();

    //specific WithOne(p => p.productCategory) to remove auto-generate foreign key
    //builder.HasOne(p => p.productShipCost).WithOne(p => p.productCategory)
    //  .HasForeignKey<ProductShipCost>(sc => sc.productCategoryId)
    //  .OnDelete(DeleteBehavior.Restrict);
  }
}
