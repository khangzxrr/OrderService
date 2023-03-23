using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderPaymentByIdAndUserIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderPaymentByIdAndUserIdSpec(int orderId, int userId)
  {
    Query
      .Where(o => o.Id == orderId && o.userId == userId)
      .Include(o => o.orderPayments);
  }
}
