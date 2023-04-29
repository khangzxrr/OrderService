using Ardalis.Specification;

namespace OrderService.Core.ShipperAggregate.specifications;
public class ShipperWithOrderByUserIdSpec : Specification<Shipper>, ISingleResultSpecification
{

  public ShipperWithOrderByUserIdSpec(int shipperId)
  {
    Query
      .Where(s => s.userId == shipperId)
      .Include(s => s.OrderShippings)
        .ThenInclude(os => os.order)
          .ThenInclude(o => o.user);
  }
}
