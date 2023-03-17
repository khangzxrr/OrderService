using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.OrderAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
  public void Configure(EntityTypeBuilder<Chat> builder)
  {
    builder.HasOne(c => c.employee).WithMany().OnDelete(DeleteBehavior.Restrict);
  }
}
