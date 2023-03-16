using Ardalis.Result;
using OrderService.Core.OrderAggregate;

namespace OrderService.Core.Interfaces;
public interface IPaymentService
{
  public Result<string> GeneratePaymentUrl(Order order);
}
