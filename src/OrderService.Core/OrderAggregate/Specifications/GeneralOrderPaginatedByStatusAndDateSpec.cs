using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class GeneralOrderPaginatedByStatusAndDateSpec: Specification<Order>
{
  public GeneralOrderPaginatedByStatusAndDateSpec(int skip, int take, DateTime? startDate, DateTime? endDate, OrderStatus status)
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
      .Include(o => o.user)
      .Where(o => o.orderDate.Date >= startDate.Value.Date && o.orderDate.Date <= endDate.Value.Date && o.status == status)
      .Skip(skip)
      .Take(take);
  }
}
