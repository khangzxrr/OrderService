using OrderService.Core.ContributorAggregate;
using OrderService.Core.ProjectAggregate;
using OrderService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.Core.UserAggregate;
using OrderService.Core.CurrencyAggregate;

namespace OrderService.Web;

public static class SeedData
{


  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {


      PopulateTestData(dbContext);


    }
  }

  public static void PopulateCurrencies(AppDbContext dbContext)
  {
    var US = new CurrencyExchange("US", 24.5f);

    dbContext.CurrencyExchanges.Add(US);
  }

  public static void PopulateRoles(AppDbContext dbContext)
  {
    var admin = new Role(nameof(RoleEnum.ADMIN));
    var customer = new Role(nameof(RoleEnum.CUSTOMER));
    var employee = new Role(nameof(RoleEnum.EMPLOYEE));
    var shipper = new Role(nameof(RoleEnum.SHIPPER));
    var manager = new Role(nameof(RoleEnum.MANAGER));

    dbContext.Roles.Add(admin);
    dbContext.Roles.Add(customer);
    dbContext.Roles.Add(employee);
    dbContext.Roles.Add(shipper);
    dbContext.Roles.Add(manager);
    
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {
    if (!dbContext.Roles.Any())
    {
      PopulateRoles(dbContext);
    }
    if (!dbContext.CurrencyExchanges.Any())
    {
      PopulateCurrencies(dbContext);
    }

    dbContext.SaveChanges();
  }
}
