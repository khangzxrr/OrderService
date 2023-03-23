using Ardalis.Result;

namespace OrderService.Core.Interfaces;
public interface IPaymentService
{
  public Task<Result<string>> GeneratePaymentUrl(int orderId, string hostName);
}
