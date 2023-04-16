using Ardalis.Result;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;

namespace OrderService.Core.Interfaces;
public interface IAddOrderDetailService
{
  public Task<Result> AddOrderDetail(Order order, int productId, int quantity);
}
