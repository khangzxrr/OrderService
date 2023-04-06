using Ardalis.Result;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.UserAggregate;

namespace OrderService.Core.Interfaces;
public interface IGetMostFreeEmployeeService
{
  public Task<Result<User>> GetMostFreeEmployee();
  public Task<Shipper?> GetMostFreeShipper();
}
