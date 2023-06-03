
using OrderService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.Core.UserAggregate;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ProductIssueAggregate;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductIssueRefundConfiguration;

namespace OrderService.Web;

public static class SeedData
{

  public static readonly User employee = new User("employee@fastship.com", "091092211", "4297f44b13955235245b2497399d7a93", "123123", "Đoàn Văn Tiến ", "157 xa lộ hà nội, quận 2, TP  Hồ Chí Minh");

  public static readonly User shipper = new User("shipper@fastship.com", "0955111533", "4297f44b13955235245b2497399d7a93", "123123", "Đinh Văn Dũng", "14 Nguyễn Tri Phương, TP Hồ Chí Minh");
  public static readonly Shipper shipperEmployee = new Shipper();

  public static readonly User customer = new User("customer@gmail.com", "091092233", "4297f44b13955235245b2497399d7a93", "123123", "Võ Ngọc Định", "1426 nguyễn duy trinh, quận 9, tp hồ chí minh");

  public static readonly User manager = new User("manager@fastship.com", "0988111737", "4297f44b13955235245b2497399d7a93", "123123", "Nguyễn Quốc Đạt ", "111 Huỳnh Tấn Phát, quận 7, tp hồ chí minh");

  public static readonly User admin = new User("admin@fastship.com", "0988111737", "4297f44b13955235245b2497399d7a93", "123123", "Đoàn Diễn Duy", "159 xa lộ hà nội, tp hồ chí minh");

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
    var AUD = new CurrencyExchange("AUD", 23.0f);

    dbContext.CurrencyExchanges.Add(US);
    dbContext.CurrencyExchanges.Add(AUD);
  }

  public static void PopulateUsers(AppDbContext dbContext)
  {
    
    employee.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.EMPLOYEE.ToString()).First());

    customer.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.CUSTOMER.ToString()).First());

    shipper.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.SHIPPER.ToString()).First());

    manager.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.MANAGER.ToString()).First());

    admin.setRole(dbContext.Roles.Where(r => r.roleName == RoleEnum.ADMIN.ToString()).First()); 

    dbContext.Users.Add(employee);
    dbContext.Users.Add(customer);
    dbContext.Users.Add(shipper);
    dbContext.Users.Add(manager);
    dbContext.Users.Add(admin);

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

  public static void PopulateIssueRefundConfig(AppDbContext dbContext)
  {
    var customerFault = new ProductIssueRefundConfiguration(ProductIssueStatus.acceptCustomerFault, 40.0f);
    var sellerFault = new ProductIssueRefundConfiguration(ProductIssueStatus.acceptSellerFault, 60.0f);
    var failToExchangeNewProduct = new ProductIssueRefundConfiguration(ProductIssueStatus.failedExchangeSellerRejectReturnToVN, 60.0f);
    var employeeFault = new ProductIssueRefundConfiguration(ProductIssueStatus.acceptEmployeeFault, 100.0f);

    dbContext.ProductIssueRefundConfigurations.Add(customerFault);
    dbContext.ProductIssueRefundConfigurations.Add(sellerFault);
    dbContext.ProductIssueRefundConfigurations.Add(employeeFault);
    dbContext.ProductIssueRefundConfigurations.Add(failToExchangeNewProduct);
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

    if (!dbContext.ProductIssueRefundConfigurations.Any())
    {
      PopulateIssueRefundConfig(dbContext);
      dbContext.SaveChanges();
    }

    dbContext.SaveChanges();
  }
}
