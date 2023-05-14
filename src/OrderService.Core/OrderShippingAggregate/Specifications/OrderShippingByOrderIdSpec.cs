using Ardalis.Specification;

namespace OrderService.Core.OrderShippingAggregate.specifications;
public class OrderShippingByOrderIdSpec : Specification<OrderShipping>, ISingleResultSpecification
{
  public OrderShippingByOrderIdSpec(int orderId)
  {
    Query
      .Where(os => os.orderId == orderId)
      .Include(os => os.shipper)
        .ThenInclude(s => s!.user);
  }
}
