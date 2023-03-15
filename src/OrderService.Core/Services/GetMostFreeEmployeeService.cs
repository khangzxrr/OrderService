
using Ardalis.Result;
using OrderService.Core.Interfaces;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.Services;
public class GetMostFreeEmployeeService : IGetMostFreeEmployeeService
{

  private readonly IRepository<User> _repository;
  
  public GetMostFreeEmployeeService(IRepository<User> repository)
  {
    _repository = repository;
  }
  
  public Result<User> GetMostFreeEmployee()
  {
    var spec = new UserByRoleSpec(RoleEnum.EMPLOYEE);
    var employee = _repository.FirstOrDefaultAsync(spec);

    if (employee == null)
    {
      return Result<User>.NotFound();
    }

    return employee;
  }
}
