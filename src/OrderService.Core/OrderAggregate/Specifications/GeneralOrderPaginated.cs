using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class GeneralOrderPaginated: Specification<Order>
{
  public GeneralOrderPaginated(int skip, int take, DateTime? startDate, DateTime? endDate)
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
      .Where(o => o.orderDate.Date >= startDate.Value.Date && o.orderDate.Date <= endDate.Value.Date)
      .Skip(skip)
      .Take(take);
  }
}
