
using OrderService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.Core.UserAggregate;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.ShipperAggregate;

namespace OrderService.Web;

public static class SeedData
{

  public static readonly User employee = new User("employee@fastship.com", "091092211", "b45cffe084dd3d20d928bee85e7b0f21", "123123", "Khang", "Ngoc", DateTime.Now, "159 aaa bbb");

  public static readonly User shipper = new User("shipper@fastship.com", "0955111533", "b45cffe084dd3d20d928bee85e7b0f21", "123123", "Khang", "Ngoc", DateTime.Now, "159 aaa bbb");
  public static readonly Shipper shipperEmployee = new Shipper("Q9", DateTime.Now, DateTime.Now, ShippingStatus.online);

  public static readonly User customer = new User("customer@gmail.com", "091092233", "4297f44b13955235245b2497399d7a93", "123123", "Khang", "Ngoc", DateTime.Now, "159 aaa bbb");

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

  public static void PopulateUsers(AppDbContext dbContext)
  {
    
    employee.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.EMPLOYEE.ToString()).First());

    customer.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.CUSTOMER.ToString()).First());

    shipper.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.SHIPPER.ToString()).First());
    dbContext.Users.Add(employee);
    dbContext.Users.Add(customer);
    dbContext.Users.Add(shipper);

    dbContext.SaveChanges();

    shipperEmployee.userId = shipper.Id;
    dbContext.Shippers.Add(shipperEmployee);
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
      PopulateUsers(dbContext);
    }

    dbContext.SaveChanges();
  }
}
