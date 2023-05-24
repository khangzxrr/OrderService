using Ardalis.Specification;

namespace OrderService.Core.OrderShippingAggregate.Specifications;
public class OrderShippingPaginatedByShipperIdSpec : Specification<OrderShipping>
{
  public OrderShippingPaginatedByShipperIdSpec(int shipperId, int skip, int take)
  {
    Query
      .Include(os => os.order)
        .ThenInclude(o => o.user)
      .Include(os => os.shipper)
        .ThenInclude(s => s!.user)
      .Where(os => os.shipper != null && os.shipper.Id == shipperId)
      .Skip(skip)
      .Take(take);
  }
}
