using Ardalis.Specification;

namespace OrderService.Core.OrderPaymentAggregate.specifications;
public class OrderPaymentByDateRangeSpec: Specification<OrderPayment>
{
  public OrderPaymentByDateRangeSpec(DateTime? startDate, DateTime? endDate)
  {

    if (startDate == null)
    {
      startDate = DateTime.MinValue;
    }

    if (endDate == null)
    {
      endDate = DateTime.MaxValue;
    }

    Query
      .Where(op => op.paymentDate >= startDate && op.paymentDate <= endDate);
  }
}
