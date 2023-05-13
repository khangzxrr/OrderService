using Ardalis.Specification;

namespace OrderService.Core.OrderPaymentAggregate.specifications;
public class OrderPaymentFilterPaginatedSpec: Specification<OrderPayment>
{
  public OrderPaymentFilterPaginatedSpec(int skip, int take, DateTime? startDate, DateTime? endDate)
  {
    if (take == 0)
    {
      take = int.MaxValue;
    }

    if (startDate == null)
    {
      startDate = DateTime.MinValue;
    }

    if (endDate == null)
    {
      endDate = DateTime.MaxValue;
    }

    Query
      .Where(op => op.paymentDate.Date >= startDate.Value.Date && op.paymentDate.Date <= endDate.Value.Date)
      .Skip(skip)
      .Take(take);
  }
}
