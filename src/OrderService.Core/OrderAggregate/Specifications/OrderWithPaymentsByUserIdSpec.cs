using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderWithPaymentsByUserIdSpec: Specification<Order>
{
  public OrderWithPaymentsByUserIdSpec(int userId)
  {
    Query
      .Include(o => o.orderPayments)
      .Where(o => o.userId == userId);
  }
}
