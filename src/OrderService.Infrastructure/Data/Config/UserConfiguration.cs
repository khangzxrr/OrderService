using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.UserAggregate;

namespace OrderService.Infrastructure.Data.Config;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(u => u.firstname).HasMaxLength(100).IsRequired();
    builder.Property(u => u.address).HasMaxLength(300).IsRequired();
    builder.Property(u => u.lastname).HasMaxLength(100).IsRequired();
    builder.Property(u => u.email).HasMaxLength(100).IsRequired();
    builder.Property(u => u.phoneNumber).HasMaxLength(100).IsRequired();
    builder.Property(u => u.passwordHash).HasMaxLength(300).IsRequired();
    builder.Property(u => u.passwordSalt).HasMaxLength(100).IsRequired();

    builder.Property(u => u.verify)
      .HasConversion(
        s => s.Value,
        s => UserVerify.FromValue(s)
      )
      .IsRequired();

    builder.HasOne(u => u.role).WithMany().IsRequired();
  }
}
