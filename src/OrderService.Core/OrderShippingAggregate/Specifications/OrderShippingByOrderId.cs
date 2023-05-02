using Ardalis.Specification;

namespace OrderService.Core.OrderShippingAggregate.specifications;
public class OrderShippingByOrderId : Specification<OrderShipping>, ISingleResultSpecification
{
  public OrderShippingByOrderId(int orderId)
  {
    Query
      .Where(os => os.orderId == orderId);
  }
}
