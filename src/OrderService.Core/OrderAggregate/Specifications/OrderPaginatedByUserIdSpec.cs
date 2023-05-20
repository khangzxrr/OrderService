using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderPaginatedByUserIdSpec : Specification<Order>
{
  public OrderPaginatedByUserIdSpec(int skip, int take, int userId)
  {
    Query
      .Include(o => o.user)
      .Where(o => o.userId == userId)
      .Skip(skip)
      .Take(take);
  }

}
