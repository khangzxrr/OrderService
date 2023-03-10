using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ProductAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductTaxConfiguration : IEntityTypeConfiguration<ProductTax>
{
  public void Configure(EntityTypeBuilder<ProductTax> builder)
  {
    builder.Property(t => t.taxName).HasMaxLength(100).IsRequired();
  }
}
