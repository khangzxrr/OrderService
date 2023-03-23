using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderPaymentByIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderPaymentByIdSpec(int orderId)
  {
    Query
      .Where(o => o.Id == orderId)
      .Include(o => o.orderPayments);
  }
}
