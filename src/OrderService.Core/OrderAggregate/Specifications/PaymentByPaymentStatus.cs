using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class PaymentByPaymentStatus : Specification<OrderPayment>, ISingleResultSpecification
{
  public PaymentByPaymentStatus(PaymentStatus paymentStatus)
  {
    Query
      .Where(op => op.paymentStatus == paymentStatus);

  }
}
