using OrderService.Core.OrderPaymentAggregate;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public record PaymentRecord(int paymentId, string paymentStatus, double paymentCost, DateTime paymentDate, string paymentDescription, string transactionId)
{
 public static PaymentRecord FromEntity(OrderPayment orderPayment)
  {
    return new PaymentRecord(orderPayment.Id, orderPayment.paymentStatus.Name, orderPayment.paymentCost, orderPayment.paymentDate, orderPayment.paymentDescription, orderPayment.transactionalId);
  }
}
