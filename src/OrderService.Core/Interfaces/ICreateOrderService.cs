using Ardalis.Result;
using OrderService.Core.OrderAggregate;

namespace OrderService.Core.Interfaces;
public interface ICreateOrderService
{
  public Task<Result<Order>> CreateNewOrder(int customerId, string orderDescription, string customerDescription, string deliveryAddress, string contactPhoneNumber);
}
