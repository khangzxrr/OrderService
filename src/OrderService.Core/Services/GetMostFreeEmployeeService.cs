
using Ardalis.Result;
using OrderService.Core.Interfaces;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ShipperAggregate.specifications;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.Services;
public class GetMostFreeEmployeeService : IGetMostFreeEmployeeService
{

  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Shipper> _shipperRepository;
  
  public GetMostFreeEmployeeService(IRepository<User> repository, IRepository<Shipper> shipperRepository)
  {
    _userRepository = repository;
    _shipperRepository = shipperRepository;
  }
  
  public async Task<Result<User>> GetMostFreeEmployee()
  {
    var spec = new UserByRoleSpec(RoleEnum.EMPLOYEE);
    var employee = await _userRepository.FirstOrDefaultAsync(spec);

    if (employee == null)
    {
      return Result<User>.NotFound();
    }

    return new Result<User>(employee!);
  }


  public async Task<Shipper?> GetMostFreeShipper()
  {
    var spec = new FreeShipperSpec();
    var shipper = await _shipperRepository.FirstOrDefaultAsync(spec);

    return shipper;
  }
}
