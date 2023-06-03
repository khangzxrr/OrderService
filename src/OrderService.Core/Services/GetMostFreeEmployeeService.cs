
using Ardalis.Result;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.Specifications;
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

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<OrderShipping> _orderShippingRepository;
  
  public GetMostFreeEmployeeService(IRepository<User> repository, IRepository<Shipper> shipperRepository, IRepository<Order> orderRepository, IRepository<OrderShipping> orderShippingRepository)
  {
    _userRepository = repository;
    _shipperRepository = shipperRepository;
    _orderRepository = orderRepository;
    _orderShippingRepository = orderShippingRepository;
  }
  
  public async Task<Result<User>> GetMostFreeEmployee()
  {
    var spec = new UserByRoleSpec(RoleEnum.EMPLOYEE);
    var employees = await _userRepository.ListAsync(spec);

    if (employees.Count == 0)
    {
      return Result<User>.Error("zero employee");
    }

    User? mostFreeEmployee = null;
    int leastOrderCount = int.MaxValue;

    foreach(var employee in employees)
    {
      var employeeSpec = new OrderByEmployeeIdSpec(employee.Id);
      var countOrders = await _orderRepository.CountAsync(employeeSpec);

      if (countOrders < leastOrderCount)
      {
        mostFreeEmployee = employee;
        leastOrderCount = countOrders;
      }

    }


    return new Result<User>(mostFreeEmployee!);
  }


  public async Task<Shipper?> GetMostFreeShipper()
  {
    var spec = new FreeShipperSpec();
    var shippers = await _shipperRepository.ListAsync(spec);

    if (shippers.Count == 0)
    {
      return Result<Shipper?>.Error("Zero shippers");
    }

    Shipper? mostFreeShipper = null;
    int leastOrderCount = int.MaxValue;

    foreach(var shipper in shippers)
    {
      var shipperSpec = new OrderShippingPaginatedByShipperIdSpec(shipper.Id, 0, int.MaxValue);
      var count = await _orderShippingRepository.CountAsync(shipperSpec);

      if (count < leastOrderCount)
      {
        mostFreeShipper = shipper;
        leastOrderCount = count;
      }
    }

    return new Result<Shipper?>(mostFreeShipper);
  }
}
