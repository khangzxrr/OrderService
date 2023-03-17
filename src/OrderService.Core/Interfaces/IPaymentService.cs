using Ardalis.Result;
using OrderService.Core.OrderAggregate;

namespace OrderService.Core.Interfaces;
public interface IPaymentService
{
  public Task<Result<string>> GeneratePaymentUrl(int orderId);
}
