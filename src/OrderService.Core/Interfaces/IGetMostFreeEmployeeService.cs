using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using OrderService.Core.UserAggregate;

namespace OrderService.Core.Interfaces;
public interface IGetMostFreeEmployeeService
{
  public Task<Result<User>> GetMostFreeEmployee();
}
