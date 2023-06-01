using System.Reflection;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderService.Core.UserAggregate;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ProductIssueAggregate;

namespace OrderService.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }


  public DbSet<User> Users => Set<User>();
  public DbSet<Shipper> Shippers => Set<Shipper>();
  public DbSet<Role> Roles => Set<Role>();

  public DbSet<CurrencyExchange> CurrencyExchanges => Set<CurrencyExchange>();

  public DbSet<ProductIssueRefundConfiguration> ProductIssueRefundConfigurations => Set<ProductIssueRefundConfiguration>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
