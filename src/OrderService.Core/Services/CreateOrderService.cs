using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using OrderService.Core.ChatAggregate;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.Services;
public class CreateOrderService : ICreateOrderService
{

  private readonly IGetMostFreeEmployeeService _getMostFreeEmployeeService;

  private readonly IRepository<User> _userRepository;

  private readonly IRepository<Order> _repository;
  public CreateOrderService(IGetMostFreeEmployeeService getMostFreeEmployeeService, IRepository<User> userRepository, IRepository<Order> repository)
  {
    _userRepository = userRepository;
    _getMostFreeEmployeeService = getMostFreeEmployeeService;
    _repository = repository;
  }

  public async Task<Result<Order>> CreateNewOrder(int customerId, string orderDescription, string customerDescription, string deliveryAddress, string contactPhoneNumber)
  {
    Guard.Against.NullOrEmpty(orderDescription);
    Guard.Against.NullOrEmpty(customerDescription);
    Guard.Against.NullOrEmpty(deliveryAddress);
    Guard.Against.NullOrEmpty(contactPhoneNumber);

    var errors = new List<ValidationError>();

    if (string.IsNullOrEmpty(orderDescription))
    {
      errors.Add(new ValidationError
      {
        Identifier = nameof(orderDescription),
        ErrorMessage = $"{nameof(orderDescription)} is required."
      });
    }

    if (string.IsNullOrEmpty(customerDescription))
    {
      errors.Add(new ValidationError
      {
        Identifier = nameof(customerDescription),
        ErrorMessage = $"{nameof(customerDescription)} is required."
      });
    }

    if (string.IsNullOrEmpty(deliveryAddress))
    {
      errors.Add(new ValidationError
      {
        Identifier = nameof(deliveryAddress),
        ErrorMessage = $"{nameof(deliveryAddress)} is required."
      });
    }

    if (string.IsNullOrEmpty(contactPhoneNumber))
    {
      errors.Add(new ValidationError
      {
        Identifier = nameof(contactPhoneNumber),
        ErrorMessage = $"{nameof(contactPhoneNumber)} is required."
      });
    }

    if (errors.Count > 0)
    {
      return Result<Order>.Invalid(errors);
    }

    var employee = _getMostFreeEmployeeService.GetMostFreeEmployee();
    if (employee == null)
    {
      return Result<Order>.Error($"{nameof(employee)} is not found (company has no employee?)");
    }

    var customer = await _userRepository.GetByIdAsync(customerId);
    if (customer == null)
    {
      return Result<Order>.Error($"{nameof(customer)} is not exist");
    }

    var order = new Order(orderDescription, customerDescription, deliveryAddress, contactPhoneNumber);

    var chat = new Chat();
    chat.SetCustomer(customer);
    chat.SetEmployee(employee);

    order.SetChat(chat);

    order = await _repository.AddAsync(order);

    await _repository.SaveChangesAsync();


    return new Result<Order>(order);
  }
}
