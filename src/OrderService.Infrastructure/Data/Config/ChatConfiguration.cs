using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.ChatAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
  public void Configure(EntityTypeBuilder<Contributor> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(100)
        .IsRequired();
  }
}
