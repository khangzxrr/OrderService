using Ardalis.Specification;
using OrderService.Core.OrderPaymentAggregate;

namespace OrderService.Core.OrderAggregate.Specifications;
public class PaymentByPaymentStatus : Specification<OrderPayment>, ISingleResultSpecification
{
  public PaymentByPaymentStatus(PaymentStatus paymentStatus)
  {
    Query
      .Where(op => op.paymentStatus == paymentStatus);

  }
}
