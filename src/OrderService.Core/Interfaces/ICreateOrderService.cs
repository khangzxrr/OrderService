using Ardalis.Result;
using OrderService.Core.OrderAggregate;

namespace OrderService.Core.Interfaces;
public interface ICreateOrderService
{
  public Task<Result<Order>> CreateNewOrder( string orderDescription, string customerDescription, string deliveryAddress, string contactPhoneNumber);
  public Task<Result<Order>> SaveNewOrder(int customerId, Order order);
}
