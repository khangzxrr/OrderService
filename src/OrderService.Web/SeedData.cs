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

  public static void PopulateEmployee(AppDbContext dbContext)
  {
    var employee = new User("employee@fastship.com", "b45cffe084dd3d20d928bee85e7b0f21", "123123", "Khang", "Ngoc", DateTime.Now, "159 aaa bbb");
    employee.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.EMPLOYEE.ToString()).First());

    var customer = new User("customer@gmail.com", "b45cffe084dd3d20d928bee85e7b0f21", "123123", "Khang", "Ngoc", DateTime.Now, "159 aaa bbb");
    customer.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.CUSTOMER.ToString()).First());

    dbContext.Users.Add(employee);
    dbContext.Users.Add(customer);
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
      dbContext.SaveChanges();
    }
    if (!dbContext.CurrencyExchanges.Any())
    {
      PopulateCurrencies(dbContext);
      dbContext.SaveChanges();
    }
    if (!dbContext.Users.Any())
    {
      PopulateEmployee(dbContext);
    }

    dbContext.SaveChanges();
  }
}
