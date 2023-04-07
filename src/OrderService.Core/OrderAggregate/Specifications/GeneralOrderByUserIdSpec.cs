using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class GeneralOrderByUserIdSpec : Specification<Order>
{
  public GeneralOrderByUserIdSpec(int userId)
  {
    Query
      .Where(o => o.userId == userId);

  }
}
