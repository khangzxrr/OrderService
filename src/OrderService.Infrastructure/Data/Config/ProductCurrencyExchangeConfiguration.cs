
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.CurrencyAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ProductCurrencyExchangeConfiguration : IEntityTypeConfiguration<ProductCurrencyExchange>
{
  public void Configure(EntityTypeBuilder<ProductCurrencyExchange> builder)
  {
    builder.HasOne(c => c.product).WithOne(p => p.currencyExchange);
    builder.HasOne(c => c.currency).WithMany();
  }
}
