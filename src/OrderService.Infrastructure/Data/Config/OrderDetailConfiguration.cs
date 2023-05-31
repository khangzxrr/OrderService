
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
  public void Configure(EntityTypeBuilder<OrderDetail> builder)
  {
  }
}
